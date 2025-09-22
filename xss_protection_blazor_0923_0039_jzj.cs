// 代码生成时间: 2025-09-23 00:39:47
using Microsoft.AspNetCore.Components;
using System.Security;
using System.Text.RegularExpressions;

namespace BlazorXssProtection
{
    // 组件用于防护XSS攻击
    public class XssProtectionComponent : ComponentBase
    {
        [Parameter]
        public string InputString { get; set; }

        // 输出字符串，用于在Blazor UI中显示
        [Parameter]
        public EventCallback<string> OnOutputStringChanged { get; set; }

        private string _outputString;

        // 用于存储输入字符串的原始值
        private string _originalInput;

        protected override void OnInitialized()
        {
            // 初始化输出字符串
            _outputString = string.Empty;
            _originalInput = string.Empty;
        }

        protected override void OnParametersSet()
        {
            if (!string.IsNullOrWhiteSpace(InputString))
            {
                TryProcessInputString(InputString);
            }
        }

        // 尝试处理输入字符串并防护XSS攻击
        private void TryProcessInputString(string input)
        {
            try
            {
                // 使用正则表达式移除潜在的XSS攻击代码
                string safeString = Regex.Replace(input, "<[^>]*>|&[^;]*;", string.Empty);

                // 更新输出字符串
                _outputString = safeString;
                _originalInput = input;

                // 触发事件回调
                OnOutputStringChanged.InvokeAsync(_outputString);
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine($"Error processing input string: {ex.Message}");
            }
        }

        // 用于在Blazor UI中显示的输出字符串
        public string OutputString
        {
            get => _outputString;
            private set => _outputString = value;
        }

        // 原始输入字符串
        public string OriginalInput
        {
            get => _originalInput;
            private set => _originalInput = value;
        }
    }
}
