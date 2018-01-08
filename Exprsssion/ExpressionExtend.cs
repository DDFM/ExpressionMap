using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Exprsssion
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
            LambdaExpression lambda = expression as LambdaExpression;
            if (lambda == null) throw new ArgumentException(nameof(expression));            
            MemberExpression memberExpr = lambda.Body as MemberExpression;
            if (memberExpr == null) throw new ArgumentException(nameof(expression));
            return memberExpr.Member.Name;
        }
    }
}
