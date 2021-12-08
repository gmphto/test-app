

using System.Data;

namespace Application.Common.Interfaces;

public interface IExcelReader
{
    DataTable ReadExcelDocument(Stream stream);
}
