using Domain.Interfaces;

namespace Infrastructure;

public class Clock : IClock
{
	public DateTimeOffset Now() => DateTimeOffset.Now;
}