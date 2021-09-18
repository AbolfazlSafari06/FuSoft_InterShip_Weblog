using System.Collections.Generic;
using Weblog.ViewModels;

namespace Weblog.Response.User
{
    public class ListOfUserResponse
    {
        public List<UserVm> Data { get; set; }
        public int TotalAnount { get; set; }

        public ListOfUserResponse(int totalAnount, List<UserVm> data)
        {
            TotalAnount = totalAnount;
            Data = data;
        }

        public ListOfUserResponse()
        {
            
        }
    }
}
