using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionMap4Net
{
    public sealed class Map<TIn, TOut>
    {
        public static Map<TIn, TOut> Instance { get; private set; }
        private static readonly ParameterExpression _parameterExpression = Expression.Parameter(typeof(TIn), "p");
        private static Func<TIn, TOut> _func = null;
        private static Dictionary<string, Expression> _dict_rule = new Dictionary<string, Expression>();

        private Map() { }

        /// <summary>
        /// 初始化
        /// </summary>
        static Map()
        {
            Instance = new Map<TIn, TOut>();
        }
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="tIn"></param>
        /// <returns></returns>
        public TOut Trans(TIn tIn)
        {
            CreatRule();
            return _func.Invoke(tIn);
        }
        /// <summary>
        /// 定义规则
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="tOut">返回<see cref="TOut"/> 属性或字段</param>
        /// <param name="from">用于生成<see cref="TOut"/>列的规则 </param>
        /// <returns></returns>
        public Map<TIn, TOut> DefineRule<TValue>(Expression<Func<TOut, TValue>> tOut, Expression<Func<TIn, TValue>> from)
        {
            var name = tOut.GetPropertyOrFieldName();
            //不同的处理结果处理
            if (!_dict_rule.ContainsKey(name))
            {
                _dict_rule.Add(name, MapExpressionVisitor.ParseToBody(from, _parameterExpression));
            }
            else
            {
                if (_dict_rule.Remove(name))
                {
                    _dict_rule.Add(name, MapExpressionVisitor.ParseToBody(from, _parameterExpression));
                }
            }
            return this;
        }
        /// <summary>
        /// 根据定义的规则生成GOTO表达式
        /// </summary>
        /// <returns></returns>
        public Map<TIn, TOut> CreatRule()
        {
            #region 创建lambda表达式
            if (_func == null)
            {
                List<MemberBinding> memberBindings = new List<MemberBinding>();
                foreach (var item in typeof(TOut).GetProperties())
                {
                    if (_dict_rule.ContainsKey(item.Name))
                    {
                        MemberBinding memberBinding = Expression.Bind(item, _dict_rule[item.Name]);
                        memberBindings.Add(memberBinding);
                    }
                    else
                    {
                        var tInProperty = typeof(TIn).GetProperty(item.Name);
                        var tInField = typeof(TIn).GetField(item.Name);
                        if (tInProperty != null || tInField != null)
                        {
                            MemberExpression property = Expression.PropertyOrField(_parameterExpression, item.Name);
                            MemberBinding memberBinding = Expression.Bind(item, property);
                            memberBindings.Add(memberBinding);
                        }

                    }
                }
                foreach (var item in typeof(TOut).GetFields())
                {
                    if (_dict_rule.ContainsKey(item.Name))
                    {
                        MemberBinding memberBinding = Expression.Bind(item, _dict_rule[item.Name]);
                        memberBindings.Add(memberBinding);
                    }
                    else
                    {
                        var tInProperty = typeof(TIn).GetProperty(item.Name);
                        var tInField = typeof(TIn).GetField(item.Name);
                        if (tInProperty != null || tInField != null)
                        {
                            MemberExpression property = Expression.PropertyOrField(_parameterExpression, item.Name);
                            MemberBinding memberBinding = Expression.Bind(item, property);
                            memberBindings.Add(memberBinding);
                        }
                    }
                }

                MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindings.ToArray());
                Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, _parameterExpression);
                _func = lambda.Compile();
            }
            #endregion

            return this;
        }
        /// <summary>
        /// 清除当前规则
        /// </summary>
        /// <returns></returns>
        public Map<TIn, TOut> Clear()
        {
            _func = null;
            _dict_rule.Clear();
            return this;
        }
    }
}
