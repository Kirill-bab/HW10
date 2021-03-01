using System;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            WebApp_Test.Setup();
            WebApp_Test.Should_Return_Program_Info_With_OK_StatusCode();
            WebApp_Test.Should_Return_OK_StatusCode_When_Number_Is_Prime();
            WebApp_Test.Should_Return_NotFound_StatusCode_When_Number_Is_Not_Prime();
            WebApp_Test.Should_Return_Not_Empty_Primes_List_With_StatusCode_OK();
            WebApp_Test.Should_Return_Empty_Primes_List_With_StatusCode_OK();
            WebApp_Test.Should_Return_Nothing_With_StatusCode_BadRequest();
        }
    }
}
