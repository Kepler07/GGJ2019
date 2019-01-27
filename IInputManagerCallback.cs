public interface IInputManagerCallback
{

    void inputSuccess(string input);
    void inputFailed(string input);

    void growUPCurrentMonster(string keyToGrowUp, float timeToWait);

    void popCurrentMonster(string input);

}