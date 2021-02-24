
/// <summary>
/// Helper struct for when the player or an enemy has some amount of something.
/// i.e. Health, Mana, Energy, etc.
/// </summary>
public class NumberPool{
	float max;
	float current;

	public NumberPool(float max){
		this.max = max;
		current = max;
	}

	public bool Full{
		get{return current >= max;}
	}
	public bool Empty{
		get{return current <= 0f;}
	}
	public float Ratio{
		get{return current / max;}
	}
	public float Amount{
		get{return current;}
	}

	public void Gain(float amount){
		current += amount;
		if(current > max) current = max;
	}

	public void Lose(float amount){
		current -= amount;
		if(current < 0f) current = 0f;
	}

	/// <summary>
	/// Attempt to use the given amount of this number pool.
	/// returns true if successful, false if unsuccessful.
	/// </summary>
	/// <param name="amount">How much of this HP or Mana to use.</param>
	/// <returns>Whether it was successful. false means there wasn't enough in the pool.</returns>
	public bool Use(float amount){
		if(current >= amount){
			current -= amount;
			return true;
		}
		return false;
	}
}