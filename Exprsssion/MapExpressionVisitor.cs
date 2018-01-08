using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Exprsssion
{
    public class MapExpressionVisitor : ExpressionVisitor
    {
        private Type _type;
        private ParameterExpression _p;

        public static Expression ParseToBody(Expression expression, Type type)
        {
            MapExpressionVisitor mapExpressionVisitor = new MapExpressionVisitor();
            mapExpressionVisitor._type = type;
            return mapExpressionVisitor.Visit(expression);
        }
        public static Expression ParseToBody(Expression expression, ParameterExpression p)
        {
            MapExpressionVisitor mapExpressionVisitor = new MapExpressionVisitor();
            mapExpressionVisitor._p = p;
            return mapExpressionVisitor.Visit(expression);
        }
        public static Expression ParseToBody(Expression expression)
        {
            MapExpressionVisitor mapExpressionVisitor = new MapExpressionVisitor();
            mapExpressionVisitor._type = null;
            return mapExpressionVisitor.Visit(expression);
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_p != null)
            {
                return _p;
            }
            if (_type != null)
            {
                return Expression.Parameter(_type, "mapParam");
            }
            return node;
        }
        protected override Expression VisitUnary(UnaryExpression node)
        {
            Expression operand = Visit(node.Operand);
            return Expression.MakeUnary(node.NodeType, operand, node.Type, node.Method);
        }
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Expression left = Visit(node.Left);
            Expression right = Visit(node.Right);
            Expression conversion = Visit(node.Conversion);
            return Expression.MakeBinary(node.NodeType, left, right, node.IsLiftedToNull, node.Method);
        }
        protected override Expression VisitMember(MemberExpression node)
        {
            Expression expression = Visit(node.Expression);
            return Expression.MakeMemberAccess(expression, node.Member);
        }

        protected override Expression VisitBlock(BlockExpression node)
        {
            List<Expression> expressions = new List<Expression>();
            List<ParameterExpression> variables = new List<ParameterExpression>();
            foreach (var item in node.Expressions)
            {
                expressions.Add(Visit(item));
            }
            foreach (var item in node.Variables)
            {
                variables.Add((ParameterExpression)Visit(item));
            }
            return Expression.Block(node.Type, variables, expressions);
        }
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            return base.VisitMemberBinding(node);
        }
        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            return base.VisitMemberMemberBinding(node);
        }
        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            return base.VisitMemberListBinding(node);
        }
        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            return base.VisitTypeBinary(node);
        }
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            return base.VisitConditional(node);
        }
        protected override Expression VisitConstant(ConstantExpression node)
        {
            return base.VisitConstant(node);
        }
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            Expression obj = Visit(node.Object);
            List<Expression> args = new List<Expression>();
            foreach (var item in node.Arguments)
            {
                args.Add(Visit(item));
            }
            return Expression.Call(obj, node.Method, args);
        }
        protected override ElementInit VisitElementInit(ElementInit node)
        {
            return base.VisitElementInit(node);
        }
        protected override Expression VisitExtension(Expression node)
        {
            return base.VisitExtension(node);
        }
        protected override Expression VisitGoto(GotoExpression node)
        {
            return base.VisitGoto(node);
        }
        protected override Expression VisitNew(NewExpression node)
        {
            return base.VisitNew(node);
        }
        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            return base.VisitCatchBlock(node);
        }
        protected override Expression VisitDefault(DefaultExpression node)
        {
            return base.VisitDefault(node);
        }
        protected override Expression VisitDynamic(DynamicExpression node)
        {
            return base.VisitDynamic(node);
        }
        protected override Expression VisitIndex(IndexExpression node)
        {
            return base.VisitIndex(node);
        }
        protected override Expression VisitInvocation(InvocationExpression node)
        {
            return base.VisitInvocation(node);
        }
        protected override Expression VisitLabel(LabelExpression node)
        {
            return base.VisitLabel(node);
        }
        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            return base.VisitLabelTarget(node);
        }
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            Expression expression = Visit(node.Body);
            return expression;

        }
        protected override Expression VisitLoop(LoopExpression node)
        {
            return base.VisitLoop(node);
        }
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            return base.VisitMemberAssignment(node);
        }
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            return base.VisitMemberInit(node);
        }
        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            return base.VisitNewArray(node);
        }
        protected override Expression VisitListInit(ListInitExpression node)
        {
            return base.VisitListInit(node);
        }
        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            return base.VisitSwitchCase(node);
        }
        protected override Expression VisitSwitch(SwitchExpression node)
        {
            return base.VisitSwitch(node);
        }
        protected override Expression VisitTry(TryExpression node)
        {
            return base.VisitTry(node);
        }
        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            return base.VisitRuntimeVariables(node);
        }
        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            return base.VisitDebugInfo(node);
        }
    }
}
