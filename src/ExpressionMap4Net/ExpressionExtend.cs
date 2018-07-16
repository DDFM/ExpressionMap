using System;
using System.Linq.Expressions;

namespace ExpressionMap4Net
{
    public static class ExpressionExtend
    {
        /// <summary>
        /// 获取lambda表达式的属性或字段名
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetPropertyOrFieldName(this Expression expression)
        {
            if (!(expression is LambdaExpression lambda)) throw new ArgumentException(nameof(expression));
            if (!(lambda.Body is MemberExpression memberExpr)) throw new ArgumentException(nameof(expression));
            return memberExpr.Member.Name;
        }
    }
}