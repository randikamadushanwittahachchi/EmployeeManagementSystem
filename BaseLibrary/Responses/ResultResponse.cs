using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Responses;

public record ResultResponse<T>(bool Flag, string Message, T? Data)
{
    public static ResultResponse<T> Success(T Data, string Message = "The process was completed successfully.") => new(true, Message, Data);
    public static ResultResponse<T> Failure(string Message) => new(false, Message, default);
}
