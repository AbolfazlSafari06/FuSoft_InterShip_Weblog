namespace Weblog
{
    public class Routing
    {
        public class Users
        {
            public const string BaseUser = "users";
            public class Get
            {
                public const string Users = "getlist";
                public const string User = "get/{id}";
            }
            public class Post
            {
                public const string Create = "create";
                public const string Update = "update";
                public const string Delete = "Delete/{id}";
            }

        }
        public class Category
        {
            public const string BaseUser = "category";
            public class Get
            {
                public const string Category = "{id}";
                public const string GetParents = "getparents/{name}";
                public const string GetListOfCategories = "categorylist/{categoryName}";
            }
            public class Post
            {
                public const string Create = "create";
                public const string Update = "update";
                public const string Delete = "Delete/{id}";
            }

            public class View
            {
                public class Get
                {
                    public const string List = "View/{perPage}";
                }
            }
        }

        public class Auth
        {
            public const string Base = "auth";

            public const string Login = "login";
            public const string Register = "register";
        }

        public class Article
        {
            public const string Base = "article";

            public class Get
            {
                public const string Detail = "{id}";
            }
            public class Post
            {
                public const string Create = "create";
                public const string Edit = "edit";
                public const string Delete = "delete/{id}";
                public const string UploadImage = "images/{name}";
            }

            public class View
            {
                public class Get
                {
                    public const string List = "view";
                }
            }
        }

        public class Comments
        {
            public const string Base = "comments";

            public class Post
            {
                public const string Create = "create";
            }

            public class Get
            {
                public const string View = "view";
            }
        }
    }
}
