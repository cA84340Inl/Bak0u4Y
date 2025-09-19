// 代码生成时间: 2025-09-19 15:42:53
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace TextFileAnalyzerApp
{
    /// <summary>
    /// A Blazor component that analyzes the content of a text file.
    /// </summary>
    public class TextFileAnalyzer : ComponentBase
    {
        [Parameter]
        public string? FilePath { get; set; }

        private string? fileContent;
        private bool isLoading = false;
        private string error = "";

        /// <summary>
        /// Reads the content of the file and analyzes it.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        protected async Task ReadFileContent()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                error = "File path is not provided.";
                StateHasChanged();
                return;
            }

            isLoading = true;
            StateHasChanged();

            try
            {
                // Read the content of the file.
                fileContent = await File.ReadAllTextAsync(FilePath);
            }
            catch (Exception ex)
            {
                error = $"An error occurred while reading the file: {ex.Message}";
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }

        /// <summary>
        /// Invoked when the component is rendered.
        /// </summary>
        public override Task SetParametersAsync(ParameterView parameters)
        {
            if (parameters.TryGetValue<string>(nameof(FilePath), out var path))
            {
                FilePath = path;
                ReadFileContent();
            }

            return base.SetParametersAsync(parameters);
        }

        /// <summary>
        /// Renders the component.
        /// </summary>
        /// <returns>The rendered component.</returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await ReadFileContent();
            }
        }
    }
}