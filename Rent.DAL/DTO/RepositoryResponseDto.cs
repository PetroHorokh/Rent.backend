using Microsoft.Data.SqlClient;

namespace Rent.DAL.DTO;

public class RepositoryResponseDto
{
    public DateTime DateTime { get; set; }
    public Exception? Error;
}