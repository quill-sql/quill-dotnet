using System;
using System.Collections.Generic;
using System.Linq;

namespace Quill.Models
{
    public static class FilterConstants
    {
        public const string IS_EXACTLY = "is exactly";
        public const string IS_NOT_EXACTLY = "is not exactly";
        public const string CONTAINS = "contains";
        public const string IS = "is";
        public const string IS_NOT = "is not";
        public const string IS_NOT_NULL = "is not null";
        public const string IS_NULL = "is null";

        public const string IN_THE_LAST = "in the last";
        public const string IN_THE_PREVIOUS = "in the previous";
        public const string IN_THE_CURRENT = "in the current";

        public const string EQUAL_TO = "equal to";
        public const string NOT_EQUAL_TO = "not equal to";
        public const string GREATER_THAN = "greater than";
        public const string LESS_THAN = "less than";
        public const string GREATER_THAN_OR_EQUAL_TO = "greater than or equal to";
        public const string LESS_THAN_OR_EQUAL_TO = "less than or equal to";
    }

    public enum StringOperator
    {
        [StringValue(FilterConstants.IS_EXACTLY)]
        IsExactly,
        [StringValue(FilterConstants.IS_NOT_EXACTLY)]
        IsNotExactly,
        [StringValue(FilterConstants.CONTAINS)]
        Contains,
        [StringValue(FilterConstants.IS)]
        Is,
        [StringValue(FilterConstants.IS_NOT)]
        IsNot
    }

    public enum DateOperator
    {
        [StringValue("custom")]
        Custom,
        [StringValue(FilterConstants.IN_THE_LAST)]
        InTheLast,
        [StringValue(FilterConstants.IN_THE_PREVIOUS)]
        InThePrevious,
        [StringValue(FilterConstants.IN_THE_CURRENT)]
        InTheCurrent,
        [StringValue(FilterConstants.EQUAL_TO)]
        EqualTo,
        [StringValue(FilterConstants.NOT_EQUAL_TO)]
        NotEqualTo,
        [StringValue(FilterConstants.GREATER_THAN)]
        GreaterThan,
        [StringValue(FilterConstants.LESS_THAN)]
        LessThan,
        [StringValue(FilterConstants.GREATER_THAN_OR_EQUAL_TO)]
        GreaterThanOrEqualTo,
        [StringValue(FilterConstants.LESS_THAN_OR_EQUAL_TO)]
        LessThanOrEqualTo
    }

    public enum NumberOperator
    {
        [StringValue(FilterConstants.EQUAL_TO)]
        EqualTo,
        [StringValue(FilterConstants.NOT_EQUAL_TO)]
        NotEqualTo,
        [StringValue(FilterConstants.GREATER_THAN)]
        GreaterThan,
        [StringValue(FilterConstants.LESS_THAN)]
        LessThan,
        [StringValue(FilterConstants.GREATER_THAN_OR_EQUAL_TO)]
        GreaterThanOrEqualTo,
        [StringValue(FilterConstants.LESS_THAN_OR_EQUAL_TO)]
        LessThanOrEqualTo
    }

    public enum NullOperator
    {
        [StringValue(FilterConstants.IS_NOT_NULL)]
        IsNotNull,
        [StringValue(FilterConstants.IS_NULL)]
        IsNull
    }

    public enum BoolOperator
    {
        [StringValue(FilterConstants.EQUAL_TO)]
        EqualTo,
        [StringValue(FilterConstants.NOT_EQUAL_TO)]
        NotEqualTo
    }

    public enum TimeUnit
    {
        [StringValue("year")]
        Year,
        [StringValue("quarter")]
        Quarter,
        [StringValue("month")]
        Month,
        [StringValue("week")]
        Week,
        [StringValue("day")]
        Day,
        [StringValue("hour")]
        Hour
    }

    public enum FieldType
    {
        [StringValue("string")]
        String,
        [StringValue("number")]
        Number,
        [StringValue("date")]
        Date,
        [StringValue("null")]
        Null,
        [StringValue("boolean")]
        Boolean
    }

    public enum FilterType
    {
        [StringValue("string-filter")]
        StringFilter,
        [StringValue("date-filter")]
        DateFilter,
        [StringValue("date-custom-filter")]
        DateCustomFilter,
        [StringValue("date-comparison-filter")]
        DateComparisonFilter,
        [StringValue("numeric-filter")]
        NumericFilter,
        [StringValue("null-filter")]
        NullFilter,
        [StringValue("string-in-filter")]
        StringInFilter,
        [StringValue("boolean-filter")]
        BooleanFilter
    }

    public class DateFilterValue
    {
        public int Value { get; set; }
        public TimeUnit Unit { get; set; }
    }

    public class DateCustomFilterValue
    {
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }

    public interface IBaseFilter
    {
        FilterType FilterType { get; set; }
        FieldType FieldType { get; set; }
        string Field { get; set; }
        string Table { get; set; }
        object Value { get; set; }
    }

    public class Filter : IBaseFilter
    {
        public FilterType FilterType { get; set; }
        public FieldType FieldType { get; set; }
        public string Field { get; set; }
        public string Table { get; set; }
        public object Value { get; set; }
        public object Operator { get; set; }

        public Filter(FilterType filterType, FieldType fieldType, string field, string table, object value, object operatorValue)
        {
            FilterType = filterType;
            FieldType = fieldType;
            Field = field ?? throw new ArgumentNullException(nameof(field));
            Table = table ?? throw new ArgumentNullException(nameof(table));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Operator = operatorValue ?? throw new ArgumentNullException(nameof(operatorValue));
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class StringValueAttribute : Attribute
    {
        public string StringValue { get; private set; }

        public StringValueAttribute(string value)
        {
            StringValue = value;
        }
    }

    public static class FilterConverter
    {
        public static IBaseFilter ConvertCustomFilter(Filter filter)
        {
            switch (filter.FilterType)
            {
                case FilterType.StringFilter:
                    if (!(filter.Value is string))
                    {
                        throw new ArgumentException($"Invalid value for StringFilter, expected string, got {filter.Value}");
                    }
                    ValidateOperator<StringOperator>(filter.Operator);
                    filter.FieldType = FieldType.String;
                    return filter;

                case FilterType.StringInFilter:
                    if (!(filter.Value is string[]))
                    {
                        throw new ArgumentException($"Invalid value for StringInFilter, expected string[], got {filter.Value}");
                    }
                    ValidateOperator<StringOperator>(filter.Operator);
                    filter.FieldType = FieldType.String;
                    return filter;

                case FilterType.NumericFilter:
                    if (!(filter.Value is double || filter.Value is int))
                    {
                        throw new ArgumentException($"Invalid value for NumericFilter, expected number, got {filter.Value}");
                    }
                    ValidateOperator<NumberOperator>(filter.Operator);
                    filter.FieldType = FieldType.Number;
                    return filter;

                case FilterType.NullFilter:
                    if (filter.Value != null)
                    {
                        throw new ArgumentException($"Invalid value for NullFilter, expected null, got {filter.Value}");
                    }
                    ValidateOperator<NullOperator>(filter.Operator);
                    filter.FieldType = FieldType.Null;
                    return filter;

                case FilterType.BooleanFilter:
                    if (!(filter.Value is bool))
                    {
                        throw new ArgumentException($"Invalid value for BooleanFilter, expected boolean, got {filter.Value}");
                    }
                    ValidateOperator<BoolOperator>(filter.Operator);
                    filter.FieldType = FieldType.Boolean;
                    return filter;

                case FilterType.DateFilter:
                    if (!(filter.Value is DateFilterValue))
                    {
                        throw new ArgumentException($"Invalid value for DateFilter, expected DateFilterValue, got {filter.Value}");
                    }
                    ValidateOperator<DateOperator>(filter.Operator);
                    filter.FieldType = FieldType.Date;
                    return filter;

                case FilterType.DateCustomFilter:
                    if (!(filter.Value is DateCustomFilterValue))
                    {
                        throw new ArgumentException($"Invalid value for DateCustomFilter, expected DateCustomFilterValue, got {filter.Value}");
                    }
                    ValidateOperator<DateOperator>(filter.Operator);
                    filter.FieldType = FieldType.Date;
                    return filter;

                case FilterType.DateComparisonFilter:
                    if (!(filter.Value is string))
                    {
                        throw new ArgumentException($"Invalid value for DateComparisonFilter, expected string, got {filter.Value}");
                    }
                    ValidateOperator<DateOperator>(filter.Operator);
                    filter.FieldType = FieldType.Date;
                    return filter;

                default:
                    throw new ArgumentException($"Unknown filter type: {filter.FilterType}");
            }
        }

        private static void ValidateOperator<T>(object operatorValue) where T : Enum
        {
            if (!Enum.IsDefined(typeof(T), operatorValue))
            {
                throw new ArgumentException($"Invalid operator for filter, expected {typeof(T).Name}, got {operatorValue}");
            }
        }
    }
}