using ManageVoyage.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ManageVoyage.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    [HttpGet]
    public IActionResult HoldBooking(string bookId)
    {
        var res = new ResponseAppDTO<string>();

        var connection = new SqlConnection("");
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "";
            command.CommandType = CommandType.StoredProcedure;
            var parameter = new SqlParameter();
            command.Parameters.Add(parameter);
            command.ExecuteNonQuery();
        }

        return Ok(res);
    }
}