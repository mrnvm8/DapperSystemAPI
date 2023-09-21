namespace DapperSystemAPI
{
    public static class ApiEndpoints
    {
        private const string ApiBase = "api";

        public static class People
        {
            private const string Base = $"{ApiBase}/people";

            public const string Create = Base;
            public const string Get = $"{Base}/{{id:guid}}";  //{{id:guid}}
            public const string GetAll = Base;
            public const string Update = $"{Base}/{{id:guid}}";
            public const string Delete = $"{Base}/{{id:guid}}";
        }
    }
}
