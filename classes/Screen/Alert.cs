using Raylib_cs;
using static Raylib_cs.Raylib;
namespace Game;
public class Alert
{
    private int _alertId = 1;
    private string _message;
    private AlertType _type;
    private double _createdAt;
    private float _durationSeconds = 3f;

    private float _rectangleX = (GetScreenWidth()/2 ) + 200;
    private float _rectangleY = 30;



    public Alert(string message, AlertType type)
    {
        _message = message;
        _type = type;
        _createdAt = GetTime();
    }

    public Alert(string message, AlertType type, float durationSeconds)
    {
        _message = message;
        _type = type;
        _durationSeconds = durationSeconds;
        _createdAt = GetTime();
    }

    public void Draw()
    {
        Color alertBg;
        Color alertText;
        switch(_type)
        {
            case AlertType.ERROR:
                alertBg = new Color(204, 0, 0);
                alertText = Color.White;
                break;
            case AlertType.WARNING:
                alertBg = new Color(255, 204, 0);
                alertText = Color.Red;
                break;
            case AlertType.INFO:
                alertBg = new Color(0, 102, 204);
                alertText = Color.White;
                break;
            default:
                alertBg = new Color(204, 0, 0);
                alertText = Color.White;
                break;
        }
        Rectangle displayRect = new Rectangle(_rectangleX, _rectangleY, 660, 50);
        DrawRectangleRec(displayRect, alertBg);
        Util.UpdateText(displayRect, _message , GetScreenWidth() - 200, 100, 30, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE, alertText);

        
    }

    public bool IsExpired()
    {
        return (GetTime() - _createdAt) >= _durationSeconds;
    }

    public void SetNewAlertId(int newId)
    {
        _alertId = newId;
        switch(_alertId)
        {
            case 2:
                _rectangleX = (GetScreenWidth()/2 ) + 200;
                _rectangleY = 70;
                break;
            case 3:
                _rectangleX = (GetScreenWidth()/2 ) + 200;
                _rectangleY = 130;
                break;
            default:
                _rectangleX = (GetScreenWidth()/2 ) + 200;
                _rectangleY = 10;
                break;
        }
    }



}