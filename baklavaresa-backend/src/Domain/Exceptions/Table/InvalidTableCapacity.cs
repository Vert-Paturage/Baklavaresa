namespace Domain.Exceptions.Table;

public class InvalidTableCapacity(int capacity) : Exception($"Table capacity {capacity} is invalid.");