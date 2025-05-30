namespace Application.Fetaures.Stores.Constants;

public static class StoresBusinessMessages
{
    public const string SectionName = "Store";

    public const string StoreNotExists = "StoreNotExists";

    public const string StoreNameAlreadyExists = "A store with the same name already exists.";
    public const string StoreEmailAlreadyExists = "A store with the same email already exists.";

    public const string StoreAlreadyVerified = "StoreAlreadyVerified";
    public const string StoreMustBeVerifiedBeforeActivation = "StoreMustBeVerifiedBeforeActivation";
    public const string StoreAlreadyActive = "StoreAlreadyActive";
}