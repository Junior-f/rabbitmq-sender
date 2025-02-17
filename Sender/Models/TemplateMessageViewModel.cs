
using System;
using System.ComponentModel.DataAnnotations;

namespace Sender.Models
{
    public class TemplateMessageViewModel
    {
        public required string PhoneNumberTo { get; set; }
        public required string Language { get; set; }
        public required string TemplateName { get; set; }
        public TemplateData? TemplateData { get; set; }
    }

    public class TemplateData
    {
        public Dictionary<string, string>? Body { get; set; }
        public Dictionary<string, Button>? Buttons { get; set; }
        public Header? Header { get; set; }
    }

    public class Button
    {
        public string Type { get; set; }
        public string Parameter { get; set; }
    }

    public class Header
    {
        public string Type { get; set; }
        public string Placeholder { get; set; }
    }
}