using System.Reflection;

namespace Domain.Entities.Abstract;

public abstract class EnumObjectAbstract<T> where T : EnumObjectAbstract<T>
{
    public byte Id {get; init;}
    public static readonly HashSet<T> All = typeof(T)
        .GetFields(BindingFlags.Public | BindingFlags.Static)
        .Select(f => (T)f?.GetValue(null)! ?? throw new SystemException())
        ?.ToHashSet() ?? throw new SystemException();
    protected EnumObjectAbstract(int id)
    {
        Id = (byte)id;
    }
    public static EnumObjectAbstract<T> FromId(ushort id)
    {
        return All.FirstOrDefault(g => g.Id == id)
            ?? throw new ArgumentException($"Invalid gender id: {id}");
    }
    #region Methods
    public override bool Equals(object? obj)
    {
        if (obj is EnumObjectAbstract<T> enumObject) { return Equals(enumObject: enumObject); }
        throw new TypeAccessException();
    }
    public bool Equals(EnumObjectAbstract<T> enumObject)
    {
        return enumObject.GetHashCode() == GetHashCode();
    }
    public override int GetHashCode() => Id;
    #endregion
}