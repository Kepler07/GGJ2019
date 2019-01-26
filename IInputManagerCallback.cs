public interface IInputManagerCallback
{

    void inputSuccess(string input);
    void inputFailed(string input);

    void growUPCurrentMonster(string keyToGrowUp);
    void growDownMonster(string keyToGrowDown);

}