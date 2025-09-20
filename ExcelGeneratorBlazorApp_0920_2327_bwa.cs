// 代码生成时间: 2025-09-20 23:27:06
using Microsoft.AspNetCore.Components;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp
{
    // 组件用于生成Excel文件
    public partial class ExcelGeneratorBlazorApp
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        // 方法：生成Excel文件
        private async Task GenerateExcelAsync(string filename)
        {
            try
            {
                // 创建一个新的XSSFWorkbook实例
                var workbook = new XSSFWorkbook();
                var sheet = workbook.CreateSheet("Sheet1");

                // 添加标题行
                var headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("Column1");
                headerRow.CreateCell(1).SetCellValue("Column2");

                // 示例数据行
                var dataRow = sheet.CreateRow(1);
                dataRow.CreateCell(0).SetCellValue("Data1");
                dataRow.CreateCell(1).SetCellValue("Data2");

                // 将Excel数据写入到内存流
                using var memoryStream = new MemoryStream();
                workbook.Write(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                // 使用JSRuntime将内存流的Excel文件下载到客户端
                await JSRuntime.InvokeVoidAsync("saveAs", memoryStream.ToArray(), filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating Excel file: " + ex.Message);
                // 你可以在这里添加错误处理逻辑
            }
        }

        // 方法：触发Excel文件生成的按钮点击事件
        private async Task OnGenerateExcelClick()
        {
            await GenerateExcelAsync("Example.xlsx");
        }
    }
}
