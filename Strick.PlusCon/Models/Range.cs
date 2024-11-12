using System;


namespace Strick.PlusCon.Models;


/// <summary>
/// Can be used to validate that a value falls within a given range.
/// </summary>
/// <typeparam name="T">Any struct type that implements the <see cref="IComparable{T}"/> interface.</typeparam>
public class Range<T> where T : struct, IComparable<T>
{
	/// <summary>
	/// Initializes a new instance with default values.
	/// </summary>
	public Range() : this(null, null) { }

	/// <summary>
	/// Initializes a new instance
	/// with <span id='min-max'>the <see cref="Min"/> and <see cref="Max"/> property 
	/// values specified by the <paramref name="min"/> and <paramref name="max"/> arguments.</span>
	/// </summary>
	/// <param name="min"><inheritdoc cref="Min" path="/summary"/></param>
	/// <param name="max"><inheritdoc cref="Max" path="/summary"/></param>
	public Range(T? min, T? max)
	{
		if (min != null && max != null)
		{
			if (min.Value.CompareTo(max.Value) < 0)
			{
				Min = min;
				Max = max;
			}
			else
			{
				Min = max;
				Max = min;
			}
		}
		else
		{
			Min = min;
			Max = max;
		}
	}


	/// <summary>
	/// The minimum acceptable value. If null (the default), the minimum is NOT checked by the <see cref="InRange(T)"/> method.
	/// </summary>
	public T? Min { get; }

	/// <summary>
	/// The maximum acceptable value. If null (the default), the maximum is NOT checked by the <see cref="InRange(T)"/> method.
	/// </summary>
	public T? Max { get; }

	/// <summary>
	/// Returns true if the <paramref name="value"/> argument is between (inclusive) 
	/// the <see cref="Min"/> and <see cref="Max"/> values.
	/// </summary>
	/// <param name="value">The value to compare to the <see cref="Min"/> and <see cref="Max"/> values.</param>
	public bool InRange(T value)
	{
		if (Min == null && Max == null)
		{ return true; }

		if (Min != null && value.CompareTo(Min.Value) < 0)
		{ return false; }

		if (Max != null && value.CompareTo(Max.Value) > 0)
		{ return false; }

		return true;
	}
}