using System.Linq.Expressions;

namespace Shared.Extensions;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> first,
        Expression<Func<T, bool>> second)
    {
        var parameter = Expression.Parameter(typeof(T));

        var leftVisitor = new ReplaceExpressionVisitor(first.Parameters[0], parameter);
        var left = leftVisitor.Visit(first.Body);

        var rightVisitor = new ReplaceExpressionVisitor(second.Parameters[0], parameter);
        var right = rightVisitor.Visit(second.Body);

        if (left is null)
        {
            throw new InvalidOperationException("Не удалось построить левую часть выражения.");
        }

        if (right is null)
        {
            throw new InvalidOperationException("Не удалось построить правую часть выражения.");
        }

        return Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(left, right),
            parameter);
    }
}

internal sealed class ReplaceExpressionVisitor(Expression oldValue, Expression newValue) : ExpressionVisitor
{
    public override Expression? Visit(Expression? node)
    {
        return node == oldValue ? newValue : base.Visit(node);
    }
}