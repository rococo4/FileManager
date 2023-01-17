using System;
using System.IO;
using System.Text;
/// <summary>
/// Главный класс программы.
/// </summary>
class Program
{
    /// <summary>
    /// Запуск основных методов.
    /// </summary>
    static void Main()
    {
        do
        {
            UsersChoosedOperation(out int numberOfOperation);
            LaunchChoosedOperation(numberOfOperation);
            Console.WriteLine("Нажмите 'G' для окончания работы, для продолжения - любую другую клавишу.");
        } while ((Console.ReadKey(true).Key != ConsoleKey.G));
    }
    
    /// <summary>
    /// Получение пути к файлу.
    /// </summary>
    /// <param name="filePath"> Путь файла.</param>
    private static void GetFileWay(out string filePath)
    {
        bool flagDir = false;
        bool flagPath = false;
        do
        {
            GetDrives(out string driveWay);
            GetDirWay(driveWay, out string dirWay, out flagDir);
            GetFilePath(dirWay, out filePath, out flagPath);
        } while ((!flagPath) || (!flagDir));
    }
    
    /// <summary>
    /// Получение папки с нужным файлом.
    /// </summary>
    /// <param name="driveWay"> Выбранный диск.</param>
    /// <param name="dirWay"> Путь к папке.</param>
    /// <param name="flagD"> Параметр для проверки корректности выбора.</param>
    private static void GetDirWay(string driveWay, out string dirWay, out bool flagD)
    {
        dirWay = driveWay;
        flagD = false;
        Console.WriteLine("Теперь выбирайте путь к папке!");
        do
        {
            string[] dir = null;
            try
            {
                dir = Directory.GetDirectories(dirWay);
            }
            catch
            {
                Console.WriteLine("Ошибка! Скорее всего в этой директории нет больше папок!");
                break;
            }
            var n = 1;
            if ((dir.Length != 0))
            {
                for (var i = 0; i < dir.Length; i++)
                {
                    Console.WriteLine($"{n}) {dir[i]}");
                    n++;
                }
                var num = 0;
                do
                {
                    Console.WriteLine($"Введите число от 1 до {n - 1} для выбора папки");
                } while (!int.TryParse(Console.ReadLine(), out num) || !(num >= 1) || !(num <= n - 1));
                dirWay = dir[num - 1];
                string output1 = "Для продолжения выбора директорий - нажмите любую клавишу, если вы считаете, что ";
                string output2 = "это нужная папка - нажмите 'F' ";
                Console.WriteLine(output1, output2);
                flagD = true;
            }
            else
            {
                Console.WriteLine("Ошибка!Скорее всего в этой директории нет больше папок!");
                break;
            }
        } while (Console.ReadKey(true).Key != ConsoleKey.F);
    }
    
    /// <summary>
    /// Получение диска, на котором находится файл.
    /// </summary>
    /// <param name="dirWay"> Диск.</param>
    private static void GetDrives(out string dirWay)
    {
        DriveInfo[] drivers = DriveInfo.GetDrives();
        var n = 1;
        Console.WriteLine("Выберите диск:");
        for (var i = 0; i < drivers.Length; i++)
        {
            Console.WriteLine($"{n}) {drivers[i]}");
            n++;
        }

        var num = 0;
        do
        {
            Console.WriteLine($"Введите число от 1 до {n - 1}");
        } while (!int.TryParse(Console.ReadLine(), out num) || !(num >= 1) || !(num <= n - 1));

        dirWay = drivers[num - 1].ToString();
    }
    
    /// <summary>
    /// Выбор файла.
    /// </summary>
    /// <param name="dirWay"> Выбранная папка.</param>
    /// <param name="fileWay"> Путь к выбранному файлу.</param>
    /// <param name="flagPath"> Параметр для проверки корректности выбора. </param>
    private static void GetFilePath(string dirWay, out string fileWay, out bool flagPath)
    {
        var num = 0;
        fileWay = null;
        string[] files = null;
        flagPath = false;
        try
        {
            files = Directory.GetFiles(dirWay);
        }
        catch
        {
            Console.WriteLine("Ошибка!!Скорее всего в этой папке нет файлов.");
        }

        var n = 1;
        if ((files != null) && (files.Length != 0))
        {
            for (var i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{n}) {files[i]}");
                n++;
            }

            do
            {
                Console.WriteLine($"Введите число от 1 до {n - 1} для выбора файла");
            } while (!int.TryParse(Console.ReadLine(), out num) || !(num >= 1) || !(num <= n - 1));

            fileWay = files[num - 1];
            Console.WriteLine($"Вы выбрали файл! Его путь:{fileWay}");
            flagPath = true;
        }
        else
        {
            Console.WriteLine("Ошибка!Скорее всего в этой папке нет файлов.");
            flagPath = false;
        }
    }

    /// <summary>
    /// Запуск всех операций.
    /// </summary>
    /// <param name="numberOfOperation"> Номер операции.</param>
    private static void LaunchChoosedOperation(int numberOfOperation)
    {
        switch (numberOfOperation)
        {
            case 1:
                WatchAndChooseDisc();
                break;
            case 2:
                WatchAndChooseFolders();
                break;
            case 3:
                WatchFiles();
                break;
            case 4:
                WriteTxtFileUTF8();
                break;
            case 5:
                WriteTxtFileAny();
                break;
            case 6:
                CopyFile(); //
                break;
            case 7:
                SwitchFilePath();
                break;
            case 8:
                DeleteFile();
                break;
            case 9:
                CreateTxtFileUTF8();
                break;
            case 10:
                CreateTxtFileAny();
                break;
            case 11:
                ConcatenationFile();
                break;
        }
    }

    /// <summary>
    /// Конкатенация двух файлов.
    /// </summary>
    private static void ConcatenationFile()
    {
        bool flagTxt = false;
        do
        {
            Console.WriteLine("Выберите путь к 1 файлу");
            GetFileWay(out string filePath1);
            Console.WriteLine("выберите путь ко 2 файлу");
            GetFileWay(out string filePath2);
            if ((Path.GetExtension(filePath1) == ".txt") && (Path.GetExtension(filePath2) == ".txt"))
            {
                try
                {
                    File.AppendAllText(filePath2, File.ReadAllText(filePath1), Encoding.UTF8);
                    flagTxt = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Ошибка!");
                }
              
            }
            else
            {
                Console.WriteLine("Выбранные файлы не .txt"!);
                flagTxt = false;
            }
        } while (!flagTxt);
    }

    /// <summary>
    /// Создание текстового файла в выбранный кодировке.
    /// </summary>
    private static void CreateTxtFileAny()
    {
        var rnd = new Random();
        string dirWay = "";
        bool flagDir = false;
        Console.WriteLine("Выберите папку где хотите создать файл.");
        do
        {
            GetDrives(out string driverWay);
            GetDirWay(driverWay, out dirWay, out flagDir);
        } while (!flagDir);
        if (dirWay != "")
        {
            Console.WriteLine("Файл создаться сам. Его имя : 6 рандомных букв.txt");
            string randomLetters = String.Empty;
            for (var i = 0; i < 6; i++)
            {
                randomLetters += (char) rnd.Next(65, 91);
            }

            randomLetters += @".txt";
            var newFileWay = dirWay + @"\" + randomLetters;
            File.Create(newFileWay).Close();
            Console.WriteLine("Введите текст который хотели бы записать.");
            var input = "";
            do
            {
                Console.WriteLine("Длина не должна превышать 10000 символов и должна иметь хоть один символ.");
                input = Console.ReadLine();
            } while ((input == null) || (input.Length > 10000) || (input.Length < 1));
            Console.WriteLine("Выберите один из предложенных вариантов кодировки:\n1)UTF32\n2)Unicode\n3)ASCII");
            int number = 0;
            do
            {
                Console.WriteLine("Нужно ввести число от 1 до 3.");
            } while ((!int.TryParse(Console.ReadLine(), out number)) || (number < 1) || (number > 3));
            switch (number)
            {
                case 1:
                    File.WriteAllText(newFileWay, input, Encoding.UTF32);
                    break;
                case 2:
                    File.WriteAllText(newFileWay, input, Encoding.Unicode);
                    break;
                case 3:
                    File.WriteAllText(newFileWay, input, Encoding.ASCII);
                    break;
            }
        }
    }

    /// <summary>
    /// Создание текстового файла в кодировке UTF8.
    /// </summary>
    private static void CreateTxtFileUTF8()
    {
        var rnd = new Random();
        string dirWay = "";
        bool flagDir = false;
        Console.WriteLine("Выберите папку где хотите создать файл.");
        do
        {
            GetDrives(out string driverWay);
            GetDirWay(driverWay, out dirWay, out flagDir);
        } while (!flagDir);

        if (dirWay != "")
        {
            Console.WriteLine("Файл создаться сам. Его имя : 6 рандомных букв .txt");
            string randomLetters = String.Empty;
            for (var i = 0; i < 6; i++)
            {
                randomLetters += (char) rnd.Next(65, 91);
            }

            randomLetters += @".txt";
            var newFileWay = dirWay + @"\" + randomLetters;
            File.Create(newFileWay).Close();
            Console.WriteLine("Введите текст который хотели бы записать.");
            var input = "";
            do
            {
                Console.WriteLine("Длина не должна превышать 10000 символов.");
                input = Console.ReadLine();
            } while ((input == null) || (input.Length > 10000));

            File.WriteAllText(newFileWay, input, Encoding.UTF8);
        }
    }
    /// <summary>
    /// Удаление файла.
    /// </summary>
    private static void DeleteFile()
    {
        GetFileWay(out string toBeDeleted);
        try
        {
            File.Delete(toBeDeleted);
            Console.WriteLine($"Файл с адресом: {toBeDeleted} - удален");
        }
        catch(Exception)
        {
            Console.WriteLine("Ошибка! попрубйте заново!");
        }
    }

    /// <summary>
    /// Изменение пути файла.
    /// </summary>
    private static void SwitchFilePath()
    {
        bool flagExist = false;
        do
        {
            bool flagDir = false;
            string destDir = "";
            Console.WriteLine("Выберите путь к файлу, который нужно переместить.");
            GetFileWay(out string sourceFile);
            Console.WriteLine("Выберите путь к папке, в которую хотите поместить файл.");
            do
            {
                GetDrives(out string driver);
                GetDirWay(driver, out destDir, out flagDir);
            } while (!flagDir);

            try
            {
                File.Move(sourceFile, destDir + @"\" + Path.GetFileName(sourceFile));
                flagExist = true;
            }
            catch (IOException)
            {
                Console.WriteLine("Файл уже сущесвтует");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine(" Ошибка! В доступе отказано!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Вот ее текст:" + ex);
            }

            if (flagExist)
                Console.WriteLine($"Ваш файл:{sourceFile} был перемещен в папку {destDir}");
        } while (!flagExist);
    }

    /// <summary>
    /// Копирование файла в другую директорию.
    /// </summary>
    private static void CopyFile()
    {
        bool flagExist = false;
        do
        {
            bool flagDir = false;
            string destDir = "";
            Console.WriteLine("Выберите путь к файлу, который нужно скопировать.");
            GetFileWay(out string sourceFile);
            Console.WriteLine("Выберите путь к папке, в которую хотите скопировать файл.");
            do
            {
                GetDrives(out string driver);
                GetDirWay(driver, out destDir, out flagDir);
            } while (!flagDir);

            try
            {
                File.Copy(sourceFile, destDir + @"\" + Path.GetFileName(sourceFile));
                flagExist = true;
            }
            catch (IOException)
            {
                Console.WriteLine("Файл уже сущесвтует");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine(" Ошибка! В доступе отказано!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка! Вот ее текст:" + ex);
            }

            if (flagExist)
                Console.WriteLine($"Ваш файл:{sourceFile} был скопирован в папку {destDir}");
        } while (!flagExist);
    }

    /// <summary>
    /// Вывод содержимого текстового файла в консоль в выбранной кодировке.
    /// </summary>
    private static void WriteTxtFileAny()
    {
        var number = 0;
        string[] codings = {"1) UTF32", "2) Unicode", "3) ASCII"};
        Console.WriteLine("Выберите кодировку:");
        foreach (var el in codings)
            Console.WriteLine(el);
        do
        {
            Console.WriteLine("Нужно ввести число от 1 до 3");
        } while ((!int.TryParse(Console.ReadLine(), out number)) || ((number < 1) || (number > 3)));
        bool flagTxt = false;
        do
        {
            GetFileWay(out string filePath);

            if (Path.GetExtension(filePath) == ".txt")
            {
                switch (number)
                {
                    case 1:
                        Console.WriteLine(File.ReadAllText(filePath, Encoding.UTF32));
                        break;
                    case 2:
                        Console.WriteLine(File.ReadAllText(filePath, Encoding.Unicode));
                        break;
                    case 3:
                        Console.WriteLine(File.ReadAllText(filePath, Encoding.ASCII));
                        break;
                }

                flagTxt = true;
            }
            else
            {
                Console.WriteLine("Выбранный файл не .txt"!);
                flagTxt = false;
            }
        } while (!flagTxt);
    }

    /// <summary>
    /// Вывод содержимого текстового файла в консоль в кодировке UTF8.
    /// </summary>
    private static void WriteTxtFileUTF8()
    {
        bool flagTxt = false;
        do
        {
            GetFileWay(out string filePath);
            if (Path.GetExtension(filePath) == ".txt")
            {
                Console.WriteLine(File.ReadAllText(filePath, Encoding.UTF8));
                flagTxt = true;
            }
            else
            {
                Console.WriteLine("Выбранный файл не .txt"!);
                flagTxt = false;
            }
        } while (!flagTxt);
    }

    /// <summary>
    /// Просмотр и выбор папки.
    /// </summary>
    private static void WatchAndChooseFolders()
    {
        bool flagDir = false;
        string dirWay = "";
        do
        {
            GetDrives(out string driveWay);
            GetDirWay(driveWay, out dirWay, out flagDir);
        } while (!flagDir);

        if (dirWay != "")
            Console.WriteLine($"Вы выбрали папку с путем : {dirWay}");
        else
            Console.WriteLine("Ошибка!");
    }

    /// <summary>
    /// Прсмотр и выбор диска.
    /// </summary>
    private static void WatchAndChooseDisc()
    {
        GetDrives(out string driveWay);
        Console.WriteLine($"Вы выбрали диск : {driveWay}");
    }

    /// <summary>
    /// Просмотр файлов в выбранной директории.
    /// </summary>
    private static void WatchFiles()
    {
        var flagPath = false;
        var flagDir = false;
        GetDrives(out string driveWay);
        do
        {
            GetDirWay(driveWay, out string dirWay, out flagDir);
            string[] files = null;
            try
            {
                files = Directory.GetFiles(dirWay);
            }
            catch
            {
                Console.WriteLine("Ошибка!");
            }

            var n = 1;
            if ((files != null) && (files.Length != 0))
            {
                for (var i = 0; i < files.Length; i++)
                {
                    Console.WriteLine($"{n}) {files[i]}");
                    n++;
                }

                flagPath = true;
            }
            else
            {
                Console.WriteLine("Ошибка!");
                flagPath = false;
            }
        } while ((!flagPath) || (!flagDir));
    }
    
    /// <summary>
    /// Выбор операции.
    /// </summary>
    /// <param name="numberOfOperation"> Номер выбранной операции.</param>
    private static void UsersChoosedOperation(out int numberOfOperation)
    {
        Console.WriteLine(@"
Перед тестированием просьба отметить, что программа тестировалась только на Windows, поэтому работу всех функций на Mac
или Linux не могу гарантировать.
Выберите одну из операций:
1. просмотр списка дисков компьютера и выбор диска;
2. переход в другую директорию;
3. просмотр списка файлов в директории;
4. вывод содержимого текстового файла в консоль в кодировке UTF-8;
5. вывод содержимого текстового файла в консоль в выбранной пользователем кодировке ;
6. копирование файла;
7. перемещение файла в выбранную пользователем директорию;
8. удаление файла;
9. создание простого текстового файла в кодировке UTF-8;
10.создание простого текстового файла в выбранной пользователем кодировке;
11.конкатенация содержимого двух или более текстовых файлов и вывод результата в консоль в кодировке UTF-8.
            ");
        int number = 0;
        do
        {
            Console.WriteLine("Нужно ввести число от 1 до  11!");
        } while ((!int.TryParse(Console.ReadLine(), out number)) || ((number < 1) || (number > 11)));

        numberOfOperation = number;
    }
}