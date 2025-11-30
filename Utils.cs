using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raymath;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace Game;

public enum AssignType
{
    ResourceArea,
    WorkPlace,
    Defense
}

public static class Util
{
    private static bool _randomAssign = true;
    private static List<ResourcePerson> _resourcePersons = new List<ResourcePerson>();
    private static int _currentPersonDisplayPage = 0;
    private static bool _peopleListOpen = false;
    private static bool _assignListOpen = false;

    public static bool IsMouseClickedOver(BaseObj obj, MouseButton button = MouseButton.Left)
    {
        Vector2 mousePos = GetMousePosition();
        return mousePos.X > obj.X && mousePos.X < obj.X + obj.Width &&  mousePos.Y > obj.Y && mousePos.Y < obj.Y + obj.Height && IsMouseButtonPressed(button);
    }

    public static void ScaledDrawTexture(Texture2D texture, float x, float y, int expectedWidth)
    {
       int frameWidth = texture.Width;
        int frameHeight = texture.Height;

        //srcRectangle
        Rectangle sourceRec = new Rectangle( 0, 0, (float)frameWidth, (float)frameHeight );
        float scale = (float)expectedWidth / frameWidth;

        Rectangle destRec = new Rectangle( x, y, frameWidth * scale,  frameHeight * scale);

         // Origin of the texture (rotation/scale point), it's relative to destination rectangle size
        Vector2 origin = new Vector2(0);
        DrawTexturePro(texture, sourceRec, destRec, origin, 0, Color.White);   
    }

    public static void ScaledDrawTexture(Texture2D texture, float x, float y, int expectedWidth, int expectedHeight)
    {
        int frameWidth = texture.Width;
        int frameHeight = texture.Height;

        //srcRectangle
        Rectangle sourceRec = new Rectangle( 0, 0, (float)frameWidth, (float)frameHeight );
        float scaleX = (float)expectedWidth / frameWidth;
        float scaleY = (float)expectedHeight / frameHeight;
        //destRectangle
        Rectangle destRec = new Rectangle( x, y, expectedWidth * scaleX, expectedHeight * scaleY);

        // Origin of the texture (rotation/scale point), it's relative to destination rectangle size
        Vector2 origin = new Vector2(0);
        DrawTexturePro(texture, sourceRec, destRec, origin, 0, Color.White);  
    }

    public static void UpdateText(string text, int x, int y, int fontSize)
    {
        DrawText($"{text}", x, y, fontSize,  Color.Black);
    }
    public static void UpdateText(Rectangle rect, string text, int x, int y, int fontSize, int hAlign, int vAlign)
    {
        Vector2 textSize = MeasureTextEx(GetFontDefault(), text, fontSize, fontSize*.1f);
        Vector2 textPos = new Vector2(
            (rect.X + Lerp(0.0f, rect.Width  - textSize.X, hAlign * 0.5f)),
            (rect.Y + Lerp(0.0f, rect.Height - textSize.Y, vAlign * 0.5f))
            );
        // Draw the text
        DrawTextEx(GetFontDefault(), text, textPos, fontSize, fontSize*.1f, Color.Black);        
        // DrawText($"{text}", x, y, fontSize,  Color.Black);
    }

    public static void UpdateText(Rectangle rect, string text, int x, int y, int fontSize, int hAlign, int vAlign, Color color)
    {
        Vector2 textSize = MeasureTextEx(GetFontDefault(), text, fontSize, fontSize*.1f);
        Vector2 textPos = new Vector2(
            (rect.X + Lerp(0.0f, rect.Width  - textSize.X, hAlign * 0.5f)),
            (rect.Y + Lerp(0.0f, rect.Height - textSize.Y, vAlign * 0.5f))
            );
        // Draw the text
        DrawTextEx(GetFontDefault(), text, textPos, fontSize, fontSize*.1f, color);        
        // DrawText($"{text}", x, y, fontSize,  Color.Black);
    }

    public static void UpdateText(string text, int x, int y, int fontSize,  Color color)
    {
        DrawText($"{text}", x, y, fontSize, color);
    }

    public static void MakeButton(Rectangle rect,  string text, int x, int y, int fontSize, int hAlign, int vAlign, Color color, Action buildButton)
    {
        Vector2 textSize = MeasureTextEx(GetFontDefault(), text, fontSize, fontSize*.1f);
        Vector2 textPos = new Vector2(
            (rect.X + Lerp(0.0f, rect.Width  - textSize.X, hAlign * 0.5f)),
            (rect.Y + Lerp(0.0f, rect.Height - textSize.Y, vAlign * 0.5f))
            );
        // Draw the text
        DrawTextEx(GetFontDefault(), text, textPos, fontSize, fontSize*.1f, color);  

        if(GetMousePosition().X > rect.X && GetMousePosition().X < rect.X + rect.Width &&  GetMousePosition().Y > rect.Y && GetMousePosition().Y < rect.Y + rect.Height && IsMouseButtonPressed(MouseButton.Left))
        {
            buildButton();
        }  
    }

    public static void OpenPeopleList()
    {
        _currentPersonDisplayPage = 0;
        _peopleListOpen = true;
    }

    public static void OpenAssignList()
    {
        _currentPersonDisplayPage = 0;
        _assignListOpen = true;
    }

    public static bool PeopleListOpen
    {
        get { return _peopleListOpen; }
    }

    public static bool AssignListOpen
    {
        get { return _assignListOpen; }
    }

    public static void PeopleList(string title, List<Person> persons)
    {
        int personLimitPerPage = 5;
        int currentPerson = 0;
        int listWidth = 1100;
        int listHeight = 600;
        Rectangle peopleListRect = new Rectangle((GetScreenWidth()  - listWidth ) / 2, (GetScreenHeight() - listHeight) / 2, listWidth, listHeight);
        DrawRectangleRec(peopleListRect,  new Color(200, 200, 200, 225));

        Util.UpdateText(peopleListRect, $"\n{title}", (int) peopleListRect.X, (int) peopleListRect.Y + 20, 30, (int) TextAlign.TEXT_ALIGN_MIDDLE, (int) TextAlign.TEXT_ALIGN_TOP);

        Rectangle closeBtnRect = new Rectangle(peopleListRect.X + peopleListRect.Width - 45, peopleListRect.Y, 45, 45);
        DrawRectangleRec(closeBtnRect, new Color(255, 100, 100, 200));
        Util.ScaledDrawTexture(RunTime.CloseIcon, closeBtnRect.X, closeBtnRect.Y, 45);

        if(GetMousePosition().X > closeBtnRect.X && GetMousePosition().X < closeBtnRect.X + closeBtnRect.Width &&  GetMousePosition().Y > closeBtnRect.Y && GetMousePosition().Y < closeBtnRect.Y + closeBtnRect.Height && IsMouseButtonPressed(MouseButton.Left))
        {
            _currentPersonDisplayPage = 0;
            _peopleListOpen = false;
        }

        Rectangle personOne = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 80, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personOne, new Color(217, 217, 219, 255));

        Rectangle personTwo = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 170, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personTwo, new Color(217, 217, 219, 255));

        Rectangle personThree = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 260, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personThree, new Color(217, 217, 219, 255));

        Rectangle personFour = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 350, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personFour, new Color(217, 217, 219, 255));

        Rectangle personFive = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 440, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personFive, new Color(217, 217, 219, 255));

        Rectangle[] personRectangles = new Rectangle[] { personOne, personTwo, personThree, personFour, personFive };

        // List<Person> persons = RunTime.gameScreen.GetPersonLists();

        int personCountTotal = persons.Count;
        int pageIndex = Math.Max(0, _currentPersonDisplayPage);
        int startIndex = pageIndex * personLimitPerPage;

        for (int slot = 0; slot < personLimitPerPage; slot++)
        {
            int personIndex = startIndex + slot;
            if (personIndex >= personCountTotal) break;
            Person person = persons[personIndex];
            currentPerson++;
            string sleepingStatus = person.IsSleeping ? "Sleeping" : "Awake";
            string idleStatus ;
            if(sleepingStatus == "Sleeping"){
                idleStatus = "Sleeping";
            } else {
                idleStatus = (person.IsWorking || person.NightShift) ? "Working" : "Idle";
            }

            Util.ScaledDrawTexture(RunTime.PersonDown, personRectangles[slot].X + 10, personRectangles[slot].Y + 10, 50);

            Rectangle personNameRect = new Rectangle((int) personRectangles[slot].X + 70, (int) personRectangles[slot].Y + 10, 200, 50);
            DrawRectangleRec(personNameRect, new Color(255, 204, 106, 255));
            Util.UpdateText(personNameRect, person.Name, (int) personRectangles[slot].X + 80, (int) personRectangles[slot].Y + 20, 30, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

            Rectangle statusRect = new Rectangle((int) personRectangles[slot].X + 300, (int) personRectangles[slot].Y + 10, 200, 50);
            DrawRectangleRec(statusRect, new Color(255, 204, 106, 255));
            Util.UpdateText(statusRect, idleStatus, (int) personRectangles[slot].X + 310, (int) personRectangles[slot].Y + 20, 30, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

            Rectangle energyRect = new Rectangle((int) personRectangles[slot].X + 520, (int) personRectangles[slot].Y + 10, 250, 50);
            DrawRectangleRec(energyRect, new Color(255, 204, 106, 255));
            Util.UpdateText(energyRect, $"E: {person.CurrentEnergy}%, H: {person.CurrentHealth}%", (int) personRectangles[slot].X + 560, (int) personRectangles[slot].Y + 20, 30, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

            if(person.IsWorking || person.NightShift)
            {
                Rectangle cancelRect = new Rectangle((int) personRectangles[slot].X + 790, (int) personRectangles[slot].Y + 10, 200, 50);
                DrawRectangleRec(cancelRect, new Color(255, 100, 100, 200));
                Util.UpdateText(cancelRect, "Cancel", (int) personRectangles[slot].X + 790, (int)personRectangles[slot].Y + 20, 30, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);
                if(GetMousePosition().X > cancelRect.X && GetMousePosition().X < cancelRect.X + cancelRect.Width &&  GetMousePosition().Y > cancelRect.Y && GetMousePosition().Y < cancelRect.Y + cancelRect.Height && IsMouseButtonPressed(MouseButton.Left))
                {
                    person.IsWorking = false;
                    if(person.ResourceArea != null)
                    {
                        person.ResourceArea.RemoveWorker(person);
                    }else if(person.WorkPlaceAsWorkplace != null)
                    {
                        person.WorkPlaceAsWorkplace.RemoveWorker(person);
                    }
                    person.QuitWork();
                }
            }
        }


        if ((pageIndex + 1) * personLimitPerPage < personCountTotal)
        {
            Rectangle nextBtn = new Rectangle(peopleListRect.X + peopleListRect.Width - 150, peopleListRect.Y + peopleListRect.Height - 60, 140, 50);
            DrawRectangleRec(nextBtn, new Color(217, 217, 219, 255));
            DrawRectangleLinesEx(nextBtn, 2, Color.Black);

            Util.MakeButton(nextBtn, "Next", (int)(peopleListRect.X + peopleListRect.Width - 150), (int)(peopleListRect.Y + peopleListRect.Height - 60), 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE, Color.Black, () => {
                _currentPersonDisplayPage += 1;
            });
        }

        if (pageIndex > 0)
        {
            Rectangle prevBtn = new Rectangle(peopleListRect.X + 10, peopleListRect.Y + peopleListRect.Height - 60, 140, 50);
            DrawRectangleRec(prevBtn, new Color(217, 217, 219, 255));
            DrawRectangleLinesEx(prevBtn, 2, Color.Black);
            Util.MakeButton(prevBtn, "Prev", (int)prevBtn.X, (int)prevBtn.Y, 28, (int)TextAlign.TEXT_ALIGN_CENTRE, (int)TextAlign.TEXT_ALIGN_MIDDLE, Color.Black, () => {
                if (_currentPersonDisplayPage > 0) _currentPersonDisplayPage--;
            });
        }        
    }


    public static void AssignList(AssignType assignPlace, BaseObj assignObj, string title , List<Person> currentWorkers, int requiredWorkers, Action<Person> AssignFunction)
    {

        int personLimitPerPage = 5;
        int currentPerson = 0;
        int listWidth = 1100;
        int listHeight = 600;
        Rectangle peopleListRect = new Rectangle((GetScreenWidth()  - listWidth ) / 2, (GetScreenHeight() - listHeight) / 2, listWidth, listHeight);
        DrawRectangleRec(peopleListRect,  new Color(200, 200, 200, 225));

        Util.UpdateText(peopleListRect, $"\n{title}", (int) peopleListRect.X, (int) peopleListRect.Y + 20, 30, (int) TextAlign.TEXT_ALIGN_MIDDLE, (int) TextAlign.TEXT_ALIGN_TOP);

        Rectangle closeBtnRect = new Rectangle(peopleListRect.X + peopleListRect.Width - 45, peopleListRect.Y, 45, 45);
        DrawRectangleRec(closeBtnRect, new Color(255, 100, 100, 200));
        Util.ScaledDrawTexture(RunTime.CloseIcon, closeBtnRect.X, closeBtnRect.Y, 45);

        if(GetMousePosition().X > closeBtnRect.X && GetMousePosition().X < closeBtnRect.X + closeBtnRect.Width &&  GetMousePosition().Y > closeBtnRect.Y && GetMousePosition().Y < closeBtnRect.Y + closeBtnRect.Height && IsMouseButtonPressed(MouseButton.Left))
        {
            _resourcePersons.Clear();
            _assignListOpen = false;
        }

        Rectangle personOne = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 80, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personOne, new Color(217, 217, 219, 255));

        Rectangle personTwo = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 170, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personTwo, new Color(217, 217, 219, 255));

        Rectangle personThree = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 260, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personThree, new Color(217, 217, 219, 255));

        Rectangle personFour = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 350, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personFour, new Color(217, 217, 219, 255));

        Rectangle personFive = new Rectangle(peopleListRect.X + 50, peopleListRect.Y + 440, peopleListRect.Width - 90, 70);
        DrawRectangleRec(personFive, new Color(217, 217, 219, 255));

        Rectangle[] personRectangles = new Rectangle[] { personOne, personTwo, personThree, personFour, personFive };

        // List<ResourcePerson> resourcePersons = new List<ResourcePerson>();
        List<Person> persons = RunTime.gameScreen.GetPersonLists();
        bool personExists = false;
        foreach (Person person in persons)
        {
            ResourcePerson resourcePerson = new ResourcePerson(person, person.IsWorking);

            foreach (ResourcePerson rp in _resourcePersons)
            {
                if (rp.Person == person)
                {
                    personExists = true;
                    break;
                }
            }

            if(personExists == false)
            {
                _resourcePersons.Add(resourcePerson);
            }

        }

        int personCountTotal = _resourcePersons.Count;
        int pageIndex = Math.Max(0, RunTime.gameScreen.CurrentPersonDisplayPage);
        int startIndex = pageIndex * personLimitPerPage;

        for (int slot = 0; slot < personLimitPerPage; slot++)
        {
            int personIndex = startIndex + slot;
            if (personIndex >= personCountTotal) break;
            // Person person = persons[personIndex];
            ResourcePerson resourcePerson = _resourcePersons[personIndex];
            Person person = resourcePerson.Person;
            currentPerson++;
            string idleStatus = person.IsWorking ? "Working" : "Idle";

            Util.ScaledDrawTexture(RunTime.PersonDown, personRectangles[slot].X + 10, personRectangles[slot].Y + 10, 50);

            Rectangle personNameRect = new Rectangle((int) personRectangles[slot].X + 70, (int) personRectangles[slot].Y + 10, 200, 50);
            DrawRectangleRec(personNameRect, new Color(255, 204, 106, 255));
            Util.UpdateText(personNameRect, person.Name, (int) personRectangles[slot].X + 80, (int) personRectangles[slot].Y + 20, 30, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

            Rectangle statusRect = new Rectangle((int) personRectangles[slot].X + 300, (int) personRectangles[slot].Y + 10, 200, 50);
            DrawRectangleRec(statusRect, new Color(255, 204, 106, 255));
            Util.UpdateText(statusRect, idleStatus, (int) personRectangles[slot].X + 310, (int) personRectangles[slot].Y + 20, 30, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

            Rectangle energyRect = new Rectangle((int) personRectangles[slot].X + 520, (int) personRectangles[slot].Y + 10, 250, 50);
            DrawRectangleRec(energyRect, new Color(255, 204, 106, 255));
            Util.UpdateText(energyRect, $"E: {person.CurrentEnergy}%, H: {person.CurrentHealth}", (int) personRectangles[slot].X + 560, (int) personRectangles[slot].Y + 20, 30, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE);

            Rectangle eachPersonTickRect = new Rectangle((int) personRectangles[slot].X + 900, (int) personRectangles[slot].Y + 15, 40, 40);
            DrawRectangleRec(eachPersonTickRect, new Color(215, 217, 219, 255));
            DrawRectangleLinesEx(eachPersonTickRect, 2, Color.Black);


            if(person.IsWorking || person.NightShift)
            {
                bool _isAssignedSomethingElse = false;
                switch (assignPlace)
                {
                    case AssignType.ResourceArea: 
                        if(person.ResourceArea == assignObj)
                        {
                            Util.ScaledDrawTexture(RunTime.greenTickIcon, eachPersonTickRect.X, eachPersonTickRect.Y, 40);
                        }
                        else
                        {
                            _isAssignedSomethingElse = true;
                        }
                        break;
                    case AssignType.WorkPlace:
                        if(person.WorkPlaceAsWorkplace == assignObj)
                        {
                            Util.ScaledDrawTexture(RunTime.greenTickIcon, eachPersonTickRect.X, eachPersonTickRect.Y, 40);
                        }
                        else
                        {
                            _isAssignedSomethingElse = true;
                        }
                        break;
                    case AssignType.Defense:
                        if(person.DefenseBuilding == assignObj)
                        {
                            Util.ScaledDrawTexture(RunTime.greenTickIcon, eachPersonTickRect.X, eachPersonTickRect.Y, 40);
                        }
                        else
                        {
                            _isAssignedSomethingElse = true;
                        }
                        break;
                    default:
                        break;
                }
                if(_isAssignedSomethingElse)
                {
                    Util.ScaledDrawTexture(RunTime.crossIcon, eachPersonTickRect.X, eachPersonTickRect.Y, 40);
                }
            }
            else
            {
                if (resourcePerson.IsSelected)
                {
                    Util.ScaledDrawTexture(RunTime.tickIcon, eachPersonTickRect.X, eachPersonTickRect.Y, 40);
                }
                if(GetMousePosition().X > eachPersonTickRect.X && GetMousePosition().X < eachPersonTickRect.X + eachPersonTickRect.Width &&  GetMousePosition().Y > eachPersonTickRect.Y && GetMousePosition().Y < eachPersonTickRect.Y + eachPersonTickRect.Height && IsMouseButtonPressed(MouseButton.Left))
                {
                    _randomAssign = false;
                    resourcePerson.IsSelected = !resourcePerson.IsSelected;
                    // Util.ScaledDrawTexture(RunTime.tickIcon, eachPersonTickRect.X, eachPersonTickRect.Y, 40);
                }
            }

        }

        Rectangle tickRect = new Rectangle((listWidth / 2) + 200, peopleListRect.Y + peopleListRect.Height - 65, 45, 45);
        DrawRectangleRec(tickRect, new Color(215, 217, 219, 255));
        DrawRectangleLinesEx(tickRect, 2, Color.Black);
        if(GetMousePosition().X > tickRect.X && GetMousePosition().X < tickRect.X + tickRect.Width &&  GetMousePosition().Y > tickRect.Y && GetMousePosition().Y < tickRect.Y + tickRect.Height && IsMouseButtonPressed(MouseButton.Left))
        {
            _randomAssign = !_randomAssign;
            foreach (ResourcePerson rp in _resourcePersons)
            {
                rp.IsSelected = false;
            }
        }

        if(_randomAssign)
        {
            Util.ScaledDrawTexture(RunTime.tickIcon, tickRect.X, tickRect.Y, 45);
        }
        // Util.ScaledDrawTexture(RunTime.tickIcon, tickRect.X, tickRect.Y, 45);
        Util.UpdateText("Random", (int)(tickRect.X + 60), (int)(tickRect.Y + 10), 28);


        Rectangle confirmRect = new Rectangle(tickRect.X + 200, peopleListRect.Y + peopleListRect.Height - 65, 200, 45);
        DrawRectangleRec(confirmRect, Color.Red );
        DrawRectangleLinesEx(confirmRect, 2, Color.Black);
        Util.UpdateText(confirmRect, "Confirm", (int)confirmRect.X + 100, (int)confirmRect.Y + 10, 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE, Color.White);

        if(GetMousePosition().X > confirmRect.X && GetMousePosition().X < confirmRect.X + confirmRect.Width &&  GetMousePosition().Y > confirmRect.Y && GetMousePosition().Y < confirmRect.Y + confirmRect.Height && IsMouseButtonPressed(MouseButton.Left))
        {
            if(currentWorkers.Count == requiredWorkers)
            {
                _assignListOpen = false;
                RunTime.gameScreen.AddMessage("Workers is already full.", AlertType.ERROR);
            }else if(currentWorkers.Count < requiredWorkers)
            {
                int workersNeeded = requiredWorkers - currentWorkers.Count;
                if (!_randomAssign)
                {
                    int workersSelected = _resourcePersons.Where(rp => rp.IsSelected).Count();
                    // Console.WriteLine($"Workers Selected: {workersSelected}, Workers Needed: {workersNeeded}");
                    if(workersSelected > workersNeeded)
                    {
                        RunTime.gameScreen.AddMessage("Too many workers", AlertType.ERROR);
                    }
                    else if(workersSelected == 0)
                    {
                        RunTime.gameScreen.AddMessage("No workers selected", AlertType.WARNING);
                    }
                    else
                    {
                       foreach (ResourcePerson rp in _resourcePersons)
                       {
                           if(rp.IsSelected)
                           {
                               AssignFunction(rp.Person);
                                if(RunTime.gameScreen.MainCalendar.IsDay){
                                    rp.Person.SetDestination(assignObj);
                               }
                           }
                       }
                         _resourcePersons.Clear();
                          _assignListOpen = false;
                    }
                }
                else
                {
                    List<Person> idlePersons = _resourcePersons.Where(rp => !rp.Person.IsWorking && !rp.Person.NightShift).Select(rp => rp.Person).ToList();
                    Random rand = new Random();
                    idlePersons = idlePersons.OrderBy(x => rand.Next()).ToList(); 

                    for (int i = 0; i < Math.Min(workersNeeded, idlePersons.Count); i++)
                    {
                        PlaySound(RunTime.infoSound);
                        AssignFunction(idlePersons[i]);
                        RunTime.gameScreen.AddMessage($"{idlePersons[i].Name} is assigned.", AlertType.INFO);
                        idlePersons[i].SetDestination(assignObj);
                    }

                    if(idlePersons.Count < workersNeeded)
                    {
                        RunTime.gameScreen.AddMessage("Workers are busy!", AlertType.WARNING);
                    }

                    _resourcePersons.Clear();
                    _assignListOpen = false;
                }

            }
        }

        if ((pageIndex + 1) * personLimitPerPage < personCountTotal)
        {
            Rectangle nextBtn = new Rectangle(peopleListRect.X + peopleListRect.Width - 150, peopleListRect.Y + peopleListRect.Height - 70, 140, 50);
            DrawRectangleRec(nextBtn, new Color(217, 217, 219, 255));
            DrawRectangleLinesEx(nextBtn, 2, Color.Black);

            Util.MakeButton(nextBtn, "Next", (int)(peopleListRect.X + peopleListRect.Width - 160), (int)(peopleListRect.Y + peopleListRect.Height - 60), 28, (int) TextAlign.TEXT_ALIGN_CENTRE, (int) TextAlign.TEXT_ALIGN_MIDDLE, Color.Black, () => {
                RunTime.gameScreen.CurrentPersonDisplayPage += 1;
            });
        }

        if (pageIndex > 0)
        {
            Rectangle prevBtn = new Rectangle(peopleListRect.X + 10, peopleListRect.Y + peopleListRect.Height - 70, 140, 50);
            DrawRectangleRec(prevBtn, new Color(217, 217, 219, 255));
            DrawRectangleLinesEx(prevBtn, 2, Color.Black);
            Util.MakeButton(prevBtn, "Prev", (int)prevBtn.X, (int)prevBtn.Y, 28, (int)TextAlign.TEXT_ALIGN_CENTRE, (int)TextAlign.TEXT_ALIGN_MIDDLE, Color.Black, () => {
                if (RunTime.gameScreen.CurrentPersonDisplayPage > 0) RunTime.gameScreen.CurrentPersonDisplayPage--;
            });
        }
    }

}