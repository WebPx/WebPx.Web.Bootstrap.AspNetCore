using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using WebPx.Web.Bootstrap.Configuration;
using WebPx.Web.TagHelpers;

namespace WebPx.Web.Bootstrap.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("bs:progress")]
    public class ProgressTagHelper : BootstrapTagHelper<ProgressOptions>
    {
        public ProgressTagHelper(AdapterResolver resolver, IOptions<BootstrapSettings> options, IBootstrap bootstrap) : base(resolver, options, x => x?.Progress)
        {
            Settings = ProgressOptions.Default;
            _boostrapGenerator = (IBootstrapGenerator)bootstrap;
        }

        private const float minValueDefault = 0;
        private const float maxValueDefault = 100;

        private float _value = minValueDefault;
        private float maxValue = maxValueDefault;
        private float minValue = minValueDefault;
        private IBootstrapGenerator _boostrapGenerator;
        private const string classDefault = "progress";
        private const string barClassDefault = "progress-bar";

        public string BarClass { get => Settings.BarClass; set => Settings.BarClass = value; }
        public string AnimationClass { get => Settings.AnimationClass; set => Settings.AnimationClass = value; }

        public bool DisplayValue { get; set; }

        public string FormatString { get; set; }

        public bool Animated { get; set; }

        public float MinValue
        {
            get => minValue;
            set
            {
                if (minValue != value)
                {
                    if (value > maxValue)
                        throw new ArgumentException($"The MinValue ({value}) can't be lower than MaxValue ({maxValue})");
                    minValue = value;
                    if (minValue > _value)
                        _value = minValue;
                }
            }
        }

        public float MaxValue
        {
            get => maxValue;
            set
            {
                if (maxValue != value)
                {
                    if (value < minValue)
                        throw new ArgumentException($"The MaxValue ({value}) can't be lower than MinValue ({minValue})");
                    maxValue = value;
                    if (maxValue < _value)
                        _value = maxValue;
                }
            }
        }

        public float Value
        {
            get => _value;
            set
            {
                if (this._value != value)
                {
                    if (value < 0 || value > 100)
                        throw new ArgumentException($"Invalid value {value}, must be between {minValue} and {maxValue}.");
                    this._value = value;
                }
            }
        }

        protected override void DoProcess(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            if (string.IsNullOrEmpty(output.Attributes["class"]?.Value.ToString() ?? null))
                output.Attributes.Add("class", classDefault);
            var barClass = BarClass;
            if (Animated)
                barClass = string.IsNullOrEmpty(barClass) ? AnimationClass : $"{barClass} {AnimationClass}";
            var percentage = (_value - minValue) / (maxValue - minValue) * 100;
            output.Content.AppendHtml("<div");
            output.Content.AppendHtml($" class=\"{barClass}\"");
            output.Content.AppendHtml($" style=\"width:{Value:#,##0.00}%\"");
            output.Content.AppendHtml(">");
            if (DisplayValue)
                output.Content.AppendHtml(string.Format(this.FormatString, percentage));
            output.Content.AppendHtml("</div>");
        }
    }
}
