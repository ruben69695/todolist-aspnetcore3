namespace Services.Results {
    public static class UserOperationCodes {
        public const int Created = 1;
        public const int Updated = 2;
        public const int Retrieved = 3;
        public const int Removed = 4;
        public const int Found = 5;
        public const int NotFound = 6;
    }
    
    public static class TodoListOperationCodes {
        public const int Created = 1;
        public const int Updated = 2;
        public const int Retrieved = 3;
        public const int Removed = 4;
        public const int Found = 5;
        public const int NotFound = 6;
        public const int EmptyUserIdentification = 7;
        public const int EmptyName = 8;
        public const int ItemAlreadyDone = 9;
        public const int IdAlreadyInUse = 10;
        public const int EmptyId = 11;
        public const int NullItem = 12;
        public const int IdViolation = 13;

    }
}