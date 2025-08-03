// 代码生成时间: 2025-08-03 14:31:11
using System;
# 优化算法效率
using System.IO;
using System.Data;
# 改进用户体验
using System.Data.Entity;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ExcelGeneratorApp
{
    /// <summary>
    /// Excel表格自动生成器
# FIXME: 处理边界情况
    /// </summary>
    public class ExcelGenerator
    {
        /// <summary>
        /// 将数据写入Excel文件
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="data">要写入的数据</param>
        /// <param name="filePath">Excel文件路径</param>
        public void WriteToExcel<T>(T[] data, string filePath) where T : class
# FIXME: 处理边界情况
        {
            try
            {
                // 确保文件路径有效
                if (string.IsNullOrEmpty(filePath)) throw new ArgumentException("文件路径不能为空");

                // 创建Excel文件
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
                {
# 改进用户体验
                    // 添加工作簿和工作表
                    WorkbookPart workbookPart = document.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();
                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    worksheetPart.Worksheet = new Worksheet(new SheetData());

                    // 设置工作表的名称
# FIXME: 处理边界情况
                    Sheets sheets = workbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                    Sheet sheet = new Sheet()
                    {
                        Id = workbookPart.GetIdOfPart(worksheetPart),
                        SheetId = 1,
                        Name = "Data"
                    };
                    sheets.Append(sheet);
# TODO: 优化性能

                    // 写入数据
                    WriteDataToSheet<T>(data, worksheetPart.Worksheet);

                    // 保存工作簿
                    workbookPart.Workbook.Save();
# TODO: 优化性能
                }
            }
            catch (Exception ex)
            {
                // 错误处理
                Console.WriteLine("发生错误：" + ex.Message);
            }
        }

        /// <summary>
# NOTE: 重要实现细节
        /// 将数据写入工作表
# 优化算法效率
        /// </summary>
        /// <typeparam name="T">实体类类型</typeparam>
        /// <param name="data">要写入的数据</param>
        /// <param name="sheetData">工作表数据对象</param>
        private void WriteDataToSheet<T>(T[] data, SheetData sheetData) where T : class
        {
            // 反射获取实体类属性
            var properties = typeof(T).GetProperties().OrderBy(p => p.Name);

            // 写入标题行
# 添加错误处理
            for (int i = 0; i < properties.Length; i++)
            {
                Cell cell = new Cell() {
                    CellReference = $"A{i + 2}" + 1,
                    DataType = CellValues.String,
                    InnerXml = $"<t>{properties[i].Name}</t>"
                };
                sheetData.AppendChild(cell);
            }

            // 写入数据行
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < properties.Length; j++)
                {
# 扩展功能模块
                    Cell cell = new Cell()
                    {
                        CellReference = $"{(char)('A' + j)}{i + 2}" + 1,
                        DataType = CellValues.String,
# 添加错误处理
                        InnerXml = $"<t>{properties[j].GetValue(data[i], null)?.ToString()}</t>"
                    };
                    sheetData.AppendChild(cell);
                }
            }
# 添加错误处理
        }
    }
}
