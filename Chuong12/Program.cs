
using System.Text;
using System.Timers;
using Timer = System.Timers.Timer;


namespace Chuong12
{
    internal class Program
    {

        public delegate int PhepToan(int so1, int so2);
        public static int PhepCong(int so1, int so2)
        {
            return so1 + so2;
        }

        //multicasting
        public delegate void HanhDong(Nguoi DoiTuong);
        public delegate void BieuDien();
        public class Nguoi
        {
            public string Ten;
            public Nguoi(string pTen)
            {
                Ten = pTen;
            }
            public void Chao(Nguoi DoiTuong)
            {
                Console.WriteLine($"{Ten} noi xin chao {DoiTuong.Ten}");
            }
            public void Hat()
            {
                Console.WriteLine($"{Ten} hat lalala");
            }
        }

        //bt11

        public delegate int Comparison(int x, int y);

        public class Sorter
        {
            public static void Sort(int[] array, Comparison compare)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = i + 1; j < array.Length; j++)
                    {
                        if (compare(array[i], array[j]) > 0)
                        {
                            int temp = array[i];
                            array[i] = array[j];
                            array[j] = temp;
                        }
                    }
                }
            }

            public static int Ascending(int x, int y) => x.CompareTo(y);
        }

        //bt12

        public delegate string StringOperation(string input);

        public class StringConverter
        {
            public static string ToUpperCase(string input) => input.ToUpper();

            public static void Convert(string str, StringOperation operation)
            {
                Console.WriteLine(operation(str));
            }
        }

        //bt13

        public class Clock
        {
            public delegate void TimeChangedHandler(DateTime time);
            public event TimeChangedHandler TimeChanged;

            private Timer timer;

            public Clock()
            {
                timer = new Timer(1000);
                timer.Elapsed += OnTimedEvent;
                timer.AutoReset = true;
                timer.Enabled = true;
            }

            private void OnTimedEvent(Object source, ElapsedEventArgs e)
            {
                TimeChanged?.Invoke(DateTime.Now);
            }
        }

        public class Display
        {
            public void ShowTime(DateTime time)
            {
                Console.Clear();
                Console.WriteLine("Current Time: " + time.ToString("HH:mm:ss"));
            }
        }



        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            //vd1

            PhepToan phepTinh = new PhepToan(PhepCong);
            Console.WriteLine("Tổng là " + phepTinh(4, 5));

            //vd2 

            Nguoi svA = new Nguoi("Hung");
            Nguoi svB = new Nguoi("Duc");
            Nguoi svC = new Nguoi("Nguyen");

            HanhDong AChao = new HanhDong(svA.Chao);
            HanhDong BChao = new HanhDong(svB.Chao);
            Console.WriteLine("su dung delegate de goi ham");
            AChao(svC);
            Console.WriteLine();
            Console.WriteLine("su dung multicast de goi nhieu ham cung luc");
            HanhDong AvabChao = AChao + BChao;
            AvabChao(svC);
            Console.WriteLine();
            Console.WriteLine("sử dụng toán tử với delegate");
            AvabChao -= AChao;
            AvabChao(svC);
            Console.WriteLine();

            Console.WriteLine("Su dung multicast de goi nhieu lan mot ham");
            BieuDien DongCa = new BieuDien(svA.Hat);
            DongCa += new BieuDien(svB.Hat);
            DongCa += new BieuDien(svC.Hat);
            // Ta hoàn toàn có thể gọi một hàm nhiều lần với multicasting
            DongCa += new BieuDien(svA.Hat);
            DongCa();

            Console.ReadLine();

            // anonymous method

            HanhDong HanhDongMoi = delegate (Nguoi DoiTuong)
            {
                Console.WriteLine("Ai do lay cap vi cua " + DoiTuong.Ten);
            };
            HanhDongMoi(svA);


            // 1. Delegate cơ bản:

            //Delegate là một kiểu dữ liệu đại diện cho một hoặc nhiều phương thức có cùng chữ ký (signature). Nó cho phép gán các phương thức vào delegate và gọi chúng một cách linh hoạt, thường được sử dụng để xử lý các sự kiện hoặc thực hiện các tác vụ khác nhau mà không cần biết chính xác phương thức nào sẽ được gọi.
            //Delegate như một thuộc tính

            //2. Delegate có thể được sử dụng như một thuộc tính trong lớp. Ví dụ:

            // public class Example
            // {
            //     public delegate void Notify(); // Khai báo delegate
            //     public Notify MyDelegate { get; set; } // Thuộc tính kiểu delegate

            //     public void Execute()
            //     {
            //         MyDelegate?.Invoke(); // Gọi delegate nếu không null
            //     }
            // }

            // Ý nghĩa: Cho phép gán một hoặc nhiều phương thức vào thuộc tính delegate và gọi chúng khi cần thiết.

            //3.Thực hiện các hoạt động theo thứ tự:

            //Để thực hiện các hoạt động theo thứ tự nhất định, bạn có thể tạo một chuỗi delegate và gọi chúng lần lượt:


            //public delegate void Operation();

            //public class Process
            //{
            //    public Operation Operations { get; set; }

            //    public void Execute()
            //    {
            //        if (Operations != null)
            //        {
            //            foreach (Operation op in Operations.GetInvocationList())
            //            {
            //                op();
            //            }
            //        }
            //    }
            //}

            //4. Delegate tĩnh:

            //Công dụng: Delegate tĩnh có thể được truy cập mà không cần tạo đối tượng, hữu ích khi phương thức liên quan là tĩnh hoặc khi cần dùng delegate trên phạm vi toàn cục.
            //Khi nên và không nên: Sử dụng delegate tĩnh khi không cần duy trì trạng thái của đối tượng. Tránh sử dụng nếu cần liên kết delegate với trạng thái hoặc dữ liệu cụ thể của đối tượng.


            //5.Delegate gọi nhiều phương thức:

            //Multicast Delegate: Delegate có thể gọi nhiều phương thức. Trong C#, delegate mặc định là Multicast.
            //Chức năng hỗ trợ: += và -= được dùng để thêm hoặc xóa phương thức vào delegate.

            //6.Tất cả các delegate là Multicast?

            //Không phải tất cả delegate đều là Multicast. Một delegate chỉ trở thành Multicast khi có nhiều phương thức được gán vào nó.


            //7.Toán tử Multicast:

            //Các toán tử += và -= được sử dụng để thực hiện việc Multicast delegate.
            //Sự kiện:

            //8.Sự kiện là một cơ chế trong lập trình để thông báo rằng một hành động đã xảy ra. Sự kiện thường được sử dụng trong các ứng dụng GUI, nhưng cũng có thể xuất hiện trong bất kỳ hệ thống nào yêu cầu xử lý sự kiện không đồng bộ.



            //9.Sự kiện trong C#:

            //Các sự kiện trong C# được thực hiện thông qua delegate.


            //10.Quá trình tạo và giải quyết sự kiện:

            //Tạo sự kiện: Khai báo một delegate và một sự kiện dựa trên delegate đó.
            //Giải quyết sự kiện: Gán các phương thức xử lý sự kiện vào sự kiện và gọi chúng khi sự kiện được kích hoạt.

            //public class Publisher
            //{
            //    public delegate void Notify();
            //    public event Notify OnNotify;

            //    public void RaiseEvent()
            //    {
            //        OnNotify?.Invoke();
            //    }
            //}

            //public class Subscriber
            //{
            //    public void OnEventRaised()
            //    {
            //        Console.WriteLine("Event raised!");
            //    }
            //}

            //public static void Main()
            //{
            //    Publisher pub = new Publisher();
            //    Subscriber sub = new Subscriber();
            //    pub.OnNotify += sub.OnEventRaised;
            //    pub.RaiseEvent();
            //}


            // 11.Chương trình sắp xếp sử dụng delegate:

            int[] numbers = { 5, 3, 8, 1, 2 };
            Sorter.Sort(numbers, Sorter.Ascending);
            Console.WriteLine(string.Join(", ", numbers));


            //12.Chương trình chuyển ký tự thường thành hoa sử dụng delegate:
            string str = "hello world";
            StringConverter.Convert(str, StringConverter.ToUpperCase);

            // 13.Chương trình đồng hồ điện tử sử dụng delegate và sự kiện:

            Clock clock = new Clock();
            Display display = new Display();
            clock.TimeChanged += display.ShowTime;

            Console.ReadLine(); // Để giữ chương trình chạy

        }
    }
}

