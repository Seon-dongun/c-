using System;
using System.IO;

namespace 씨샵과제1
{
    class Computer // 컴퓨터가 가져야하는 기본적인 정보를 가짐. 컴퓨터 아이디, 컴퓨터를 렌트했을 때 렌트를 한 유저의 아이디, 현재 컴퓨터 이용가능여부, 렌트기간을 나타내는 DR, DL, DU를 갖는다.
    {
        public int Comid { get; set; }
        public int Userid { get; set; }
        public string Avail { get; set; } = "Y";
        public int DR { get; set; } = 0;
        public int DL { get; set; } = 0;
        public int DU { get; set; } = 0;
        public Computer(int comid)
        {
            this.Comid = comid;
        }
    }
    class Notebook : Computer // 컴퓨터의 기본 정보에 더해 노트북이 가져야하는 정보를 가짐. 노트북 아이디, 제공하는 서비스, 타입이름, 하루 당 이용 가격을 갖는다.
    {
        public int Noteid { get; set; }
        public string Service { get; set; } = "internet,scientific";
        public string Typename { get; set; } = "Notebook";
        public int price { get; set; } = 10000;
        public Notebook(int comid,int noteid) : base(comid)
        {
            this.Noteid = noteid;
        }
    }
    class Netbook : Computer // 컴퓨터의 기본 정보에 더해 넷북이 가져야하는 정보를 가짐. 넷북 아이디, 제공하는 서비스, 타입이름, 하루 당 이용 가격을 갖는다.
    {
        public int Netid { get; set; }
        public string Service { get; set; } = "internet";
        public string Typename { get; set; } = "Netbook";
        public int Price { get; set; } = 7000;
        public Netbook(int comid,int netid) : base(comid)
        {
            this.Netid = netid;
        }
    }
    class Desktop : Computer // 컴퓨터의 기본 정보에 더해 데스크탑이 가져야하는 정보를 가짐. 데스크탑 아이디, 제공하는 서비스, 타입이름, 하루 당 이용 가격을 갖는다.
    {
        public int Deskid { get; set; }
        public string Service { get; set; } = "internet,scientific,game";
        public string Typename { get; set; } = "Desktop";
        public int Price { get; set; } = 13000;
        public Desktop(int comid,int deskid) : base(comid)
        {
            this.Deskid = deskid;
        }
    }
    class User // 유저가 가져야하는 기본 정보를 갖는다. 이름, 유저 아이디, 렌트했을 때 렌트한 컴퓨터의 아이디, 사용자의 현재 렌트 여부를 갖는다.
    {
        public string Name { get; set; }
        public int Uid { get; set; }
        public int RentCompId { get; set; }
        public string Rent { get; set; } = "N";
        public User(string name,int uid)
        {
            this.Uid = uid;
            this.Name = name;
        }
    }
    class Students : User // 유저의 기본 정보에 더해 학생이 가져야하는 정보를 갖는다. 학생 아이디, 이용하는 서비스, 타입이름을 갖는다.
    {
        public int Sid { get; set; }
        public string UseService { get; set; } = "internet,scientific";
        public string Typename { get; set; } = "Students";
        public Students(string name,int uid, int sid) : base(name,uid)
        {
           this.Sid = sid;
        }
    }
    class Workers : User // 유저의 기본 정보에 더해 워커가 가져야하는 정보를 갖는다. 워커 아이디, 이용하는 서비스, 타입이름을 갖는다.
    {
        public int Wid { get; set; }
        public string UseService { get; set; } = "internet";
        public string Typename { get; set; } = "OfficeWorkers";
        public Workers(string name, int uid, int wid) : base(name, uid)
        {
            this.Wid = wid;
        }
    }
    class Gamers : User  // 유저의 기본 정보에 더해 게이머가 가져야하는 정보를 갖는다. 게이머 아이디, 이용하는 서비스, 타입이름을 갖는다.
    {
        public int Gid { get; set; }
        public string UseService { get; set; } = "internet,game";
        public string Typename { get; set; } = "Gamers";
        public Gamers(string name, int uid, int gid) : base(name, uid)
        {
            this.Gid = gid;
        }
    }
    class ComputerManager
    {
        private Computer[] arrComp;
        private User[] arrUser;
        protected int totalCost=0;
        public ref Computer[] getComp() // main 함수에서 arrComp를 다룰 수 있도록 참조값을 반환하는 메소드
        {
            return ref arrComp;
        }
        public ref User[] getUser() // main 함수에서 arrUser를 다룰 수 있도록 참조값을 반환하는 메소드
        {
            return ref arrUser;
        }

        public void set_Avail_Userid_DR_DL_DU(ref Computer tmp,int uid,int rentPeriod) // 컴퓨터를 렌트했을 때 정보 업데이트 메소드
        {
            tmp.Avail = "N";
            tmp.Userid = uid;
            tmp.DR = rentPeriod;
            tmp.DL = rentPeriod;
            tmp.DU = 0;
        }
        public void user_Get_Computer(StreamWriter sw,int uid,int rentPeriod) // 유저가 컴퓨터를 렌트했을 때 이를 처리하는 메소드
        {
            int rentCompId=0;
            if (arrComp.Length < uid || uid < 1) // 잘못된 유저 아이디 입력시 아무 작업하지 않고 리턴
                return;

            if(arrUser[uid-1] is Students) // 만약 렌트한 유저가 학생인 경우
            {
                for(int i=0; i < arrComp.Length; i++) // 학생이 이용가능한 컴퓨터를 탐색
                {
                    /* 학생의 경우 인터넷과 과학 서비스를 포함해야 하기 때문에 적어도 노트북과 데스크탑이 필요하다.
                       이때 노트북과 데스크탑 둘 다 이용할 수 있는 상황이라면 학생이 필요한 기능상 굳이 데스크탑까지 쓸 필요없이 
                       노트북만 써도 괜찮기때문에 이용 가능한 노트북이 있는지부터 확인하고 그 다음 데스크탑이 있는지 확인한다.
                    */
                    if(arrComp[i] is Notebook && arrComp[i].Avail == "Y") // 이용가능한 노트북이 있다면
                    {
                        set_Avail_Userid_DR_DL_DU(ref arrComp[i], uid, rentPeriod); // 렌트할 컴퓨터에 렌트한 유저의 아이디, 렌트 기간 관련 정보들을 업데이트
                        arrUser[uid - 1].RentCompId = i; // 렌트한 유저는 본인이 렌트한 컴퓨터의 아이디를 업데이트
                        rentCompId = i; // 추후 Writer를 통한 작성을 위해 렌트한 컴퓨터 아이디를 따로 저장해둠
                        arrUser[uid - 1].Rent = "Y"; //렌트한 유저의 렌트 여부 업데이트
                        break; // 렌트에 성공했으면 더 작업할 필요가 없기 때문에 break

                        /* 이후 조건문 내부 동일 코드 주석도 동일합니다*/
                    }
                    else if(arrComp[i] is Desktop && arrComp[i].Avail == "Y") // 이용가능한 데스크탑이 있다면
                    {
                        set_Avail_Userid_DR_DL_DU(ref arrComp[i], uid, rentPeriod);
                        arrUser[uid - 1].RentCompId = i;
                        rentCompId = i;
                        arrUser[uid - 1].Rent = "Y";
                        break;
                    }
                }
            }
            else if(arrUser[uid-1] is Workers) // 만약 렌트한 유저가 워커인 경우
            {
                for(int i = 0; i < arrComp.Length; i++) // 워커가 이용가능한 컴퓨터를 탐색
                {
                    /* 워커의 경우 인터넷 서비스만 있으면 되니까 넷북,노트북,데스크탑 모두 이용 가능.
                       이때 넷북,노트북,데스크탑 모두 이용할 수 있는 상황이라면 기능이 한정적인
                       넷북을 가장 우선으로 이용하고 그 다음은 노트북, 제일 마지막으로 데스크탑을 확인한다.
                    */
                    if (arrComp[i] is Netbook && arrComp[i].Avail == "Y") // 이용가능한 넷북이 있다면
                    {
                        set_Avail_Userid_DR_DL_DU(ref arrComp[i], uid, rentPeriod);
                        arrUser[uid - 1].RentCompId = i;
                        rentCompId = i;
                        arrUser[uid - 1].Rent = "Y";
                        break;
                    }
                    else if (arrComp[i] is Notebook && arrComp[i].Avail == "Y") // 이용가능한 노트북이 있다면
                    {
                        set_Avail_Userid_DR_DL_DU(ref arrComp[i], uid, rentPeriod);
                        arrUser[uid - 1].RentCompId = i;
                        rentCompId = i;
                        arrUser[uid - 1].Rent = "Y";
                        break;
                    }
                    else if (arrComp[i] is Desktop && arrComp[i].Avail == "Y") // 이용가능한 데스크탑이 있다면
                    {
                        set_Avail_Userid_DR_DL_DU(ref arrComp[i], uid, rentPeriod);
                        arrUser[uid - 1].RentCompId = i;
                        rentCompId = i;
                        arrUser[uid - 1].Rent = "Y";
                        break;
                    }
                }
            }
            else if(arrUser[uid-1] is Gamers) // 만약 렌트한 유저가 게이머인 경우
            {
                for(int i = 0; i < arrComp.Length; i++) // 게이머가 이용가능한 컴퓨터 탐색
                {
                    /*
                      게이머의 경우 인터넷과 게임 서비스가 필요하기 때문에 게임 서비스를 포함하는
                      컴퓨터인 데스크탑밖에 이용할 수 없다.
                     */
                    if (arrComp[i] is Desktop && arrComp[i].Avail == "Y") // 이용가능한 데스크탑이 있다면
                    {
                        set_Avail_Userid_DR_DL_DU(ref arrComp[i], uid, rentPeriod);
                        arrUser[uid - 1].RentCompId = i;
                        rentCompId = i;
                        arrUser[uid - 1].Rent = "Y";
                        break;
                    }
                }
            }
            sw.WriteLine($"Computer #{rentCompId+1} has been assigned to User #{uid}");
            sw.WriteLine("===============================================================================");
        }

        public void reset_Avail_Userid_DR_DL_DU(ref Computer tmp) // 렌트한 컴퓨터 반납 후 컴퓨터 정보를 업데이트하는 메소드
        {
            tmp.Avail = "Y";
            tmp.Userid = 0;
            tmp.DR = 0;
            tmp.DL = 0;
            tmp.DU = 0;
        }

        public int calculate_fee(Computer tmp) // 컴퓨터 종류에 따른 이용 가격을 책정하는 메소드
        {
            int fee=0;
            if(tmp is Notebook)
                fee = tmp.DU * ((Notebook)tmp).price;
            else if(tmp is Netbook)
                fee = tmp.DU * ((Netbook)tmp).Price;
            else if(tmp is Desktop)
                fee = tmp.DU * ((Desktop)tmp).Price;
            totalCost += fee; // 이용 금액 총 액수에 이번에 책정된 이용 금액을 추가해줌
            return fee;
        }

        public void pass_One_Day(StreamWriter sw) // 하루가 지나갔을 때 렌트 상태를 업데이트하는 메소드
        {
            sw.WriteLine("It has passed one day...");
            for (int i = 0; i < arrComp.Length; i++) // 컴퓨터를 탐색하여
            {
                if (arrComp[i].Avail == "N") // 현재 이용 가능 불가 컴퓨터, 즉 렌트가 되어 있는 컴퓨터의 경우
                {
                    arrComp[i].DL--; // 남은 대여일 수 하루 감소
                    arrComp[i].DU++; // 사용한 일 수 하루 증가
                    if (arrComp[i].DL == 0) // 만약 남은 대여일이 0일이 된 경우
                    {
                        int rentUid = arrComp[i].Userid;
                        int fee = calculate_fee(arrComp[i]); // 렌트 비용 측정
                        reset_Avail_Userid_DR_DL_DU(ref arrComp[i]); // 렌트한 컴퓨터 반납 후 컴퓨터 정보 업데이트
                        arrUser[rentUid-1].Rent = "N"; // 유저의 렌트 여부 업데이트
                        arrUser[rentUid-1].RentCompId = 0; // 유저의 렌트한 컴퓨터 아이디 정보 업데이트
                        sw.WriteLine($"Time for Computer#{i+1} has expired. User #{rentUid} has returned Computer #{i+1} and paid {fee}won.");
                    }
                }
            }
            sw.WriteLine("===============================================================================");
        }

        public void user_Return_Computer(StreamWriter sw,int returnUid) // 유저의 컴퓨터 반납을 처리하는 메소드
        {
            if (arrUser.Length < returnUid || returnUid<1) // 잘못된 유저 아이디 입력시 아무 작업하지 않고 리턴
                return;
            int returnComid = arrUser[returnUid - 1].RentCompId; // 컴퓨터 반납을 진행한 유저의 정보를 통해서 유저가 반납하는 컴퓨터의 아이디를 구한다
            int fee = calculate_fee(arrComp[returnComid]);  // 렌트 비용 측정
            reset_Avail_Userid_DR_DL_DU(ref arrComp[returnComid]); // 렌트한 컴퓨터 반납 후 컴퓨터 정보 업데이트
            arrUser[returnUid - 1].Rent = "N"; // 유저의 렌트 여부 업데이트
            arrUser[returnUid - 1].RentCompId = 0; // 유저의 렌트한 컴퓨터 아이디 정보 업데이트
            sw.WriteLine($"User #{returnUid} has returned Computer #{returnComid+1} and paid {fee}won.");
            sw.WriteLine("===============================================================================");
        }

        public void show_All(StreamWriter sw) // 현재 상태를 표시하는 메소드
        {
            sw.WriteLine($"Total Cost:{totalCost}"); // 총 지불된 금액 작성
            sw.WriteLine("Computer List:");
            
            for (int i = 0; i < arrComp.Length; i++) // 컴퓨터들을 확인하여
            {
                if (arrComp[i].Avail == "Y") // 컴퓨터가 렌트 가능할 때, 즉 렌트 되지 않은 경우 렌트 관련된 정보는 생략해서 작성한다
                {
                    if (arrComp[i] is Notebook) // 해당 객체가 Notebook 객체인 경우
                    {
                        Notebook tmp = arrComp[i] as Notebook;
                        sw.WriteLine($"({i + 1})type:{tmp.Typename}, ComId:{arrComp[i].Comid}, NoteId: {tmp.Noteid}, Used for: {tmp.Service}, Avail:{arrComp[i].Avail}");
                    }
                    else if (arrComp[i] is Netbook) // 해당 객체가 Netbook 객체인 경우
                    {
                        Netbook tmp = arrComp[i] as Netbook;
                        sw.WriteLine($"({i + 1})type:{tmp.Typename}, ComId:{arrComp[i].Comid}, NetId: {tmp.Netid}, Used for: {tmp.Service}, Avail:{arrComp[i].Avail}");
                    }
                    else if (arrComp[i] is Desktop) // 해당 객체가 Desktop 객체인 경우
                    {
                        Desktop tmp = arrComp[i] as Desktop;
                        sw.WriteLine($"({i + 1})type:{tmp.Typename}, ComId:{arrComp[i].Comid}, DeskId: {tmp.Deskid}, Used for: {tmp.Service}, Avail:{arrComp[i].Avail}");
                    }
                }
                else // 컴퓨터가 렌트 불가능할 때, 즉 렌트되어 있는 경우 렌트 관련 정보까지 같이 작성한다
                {
                    if (arrComp[i] is Notebook) // 해당 객체가 Notebook 객체인 경우
                    {
                        Notebook tmp = arrComp[i] as Notebook;
                        sw.WriteLine($"({i + 1})type:{tmp.Typename}, ComId:{arrComp[i].Comid}, NoteId: {tmp.Noteid}, Used for: {tmp.Service}, Avail:{arrComp[i].Avail} (UserId:{arrComp[i].Userid}, DR:{arrComp[i].DR}, DL:{arrComp[i].DL}, DU:{arrComp[i].DU})");
                    }
                    else if (arrComp[i] is Netbook) // 해당 객체가 Netbook 객체인 경우
                    {
                        Netbook tmp = arrComp[i] as Netbook;
                        sw.WriteLine($"({i + 1})type:{tmp.Typename}, ComId:{arrComp[i].Comid}, NetId: {tmp.Netid}, Used for: {tmp.Service}, Avail:{arrComp[i].Avail} (UserId:{arrComp[i].Userid}, DR:{arrComp[i].DR}, DL:{arrComp[i].DL}, DU:{arrComp[i].DU})");
                    }
                    else if (arrComp[i] is Desktop) // 해당 객체가 Desktop 객체인 경우
                    {
                        Desktop tmp = arrComp[i] as Desktop;
                        sw.WriteLine($"({i + 1})type:{tmp.Typename}, ComId:{arrComp[i].Comid}, DeskId: {tmp.Deskid}, Used for: {tmp.Service}, Avail:{arrComp[i].Avail} (UserId:{arrComp[i].Userid}, DR:{arrComp[i].DR}, DL:{arrComp[i].DL}, DU:{arrComp[i].DU})");
                    }
                }
            }
            sw.WriteLine("User List:");
            for(int i = 0; i < arrUser.Length; i++) // 유저들을 확인하여
            {
                if (arrUser[i].Rent == "N") // 유저가 렌트하지 않았을 때는 렌트한 컴퓨터 아이디는 생략해서 작성한다
                {
                    if (arrUser[i] is Students) // 해당 객체가 Students 인 경우
                    {
                        Students tmp = arrUser[i] as Students;
                        sw.WriteLine($"({i + 1})type:{tmp.Typename}, Name:{arrUser[i].Name}, UserId:{arrUser[i].Uid}, StudId:{tmp.Sid}, Used for:{tmp.UseService}, Rent:{arrUser[i].Rent}");
                    }
                    else if (arrUser[i] is Gamers)// 해당 객체가 Gamers 인 경우
                    {
                        Gamers tmp = arrUser[i] as Gamers;
                        sw.WriteLine($"({i + 1})type:{tmp.Typename}, Name:{arrUser[i].Name}, UserId:{arrUser[i].Uid}, GamerdId:{tmp.Gid}, Used for:{tmp.UseService}, Rent:{arrUser[i].Rent}");
                    }
                    else if (arrUser[i] is Workers)//해당 객체가 Workers 인 경우
                    {
                        Workers tmp = arrUser[i] as Workers;
                        sw.WriteLine($"({i + 1})type:{tmp.Typename}, Name:{arrUser[i].Name}, UserId:{arrUser[i].Uid}, WorkerId:{tmp.Wid}, Used for:{tmp.UseService}, Rent:{arrUser[i].Rent}");
                    }
                }
                else // 유저가 렌트한 경우 렌트한 컴퓨터 아이디까지 작성한다
                {
                    if (arrUser[i] is Students) // 해당 객체가 Students 인 경우
                    {
                        Students tmp = arrUser[i] as Students;
                        sw.WriteLine($"({i + 1})type:{tmp.Typename}, Name:{arrUser[i].Name}, UserId:{arrUser[i].Uid}, StudId:{tmp.Sid}, Used for:{tmp.UseService}, Rent:{arrUser[i].Rent} (RentCompid:{arrUser[i].RentCompId+1})");
                    }
                    else if (arrUser[i] is Gamers)// 해당 객체가 Gamers 인 경우
                    {
                        Gamers tmp = arrUser[i] as Gamers;
                        sw.WriteLine($"({i + 1})type:{tmp.Typename}, Name:{arrUser[i].Name}, UserId:{arrUser[i].Uid}, GamerdId:{tmp.Gid}, Used for:{tmp.UseService}, Rent:{arrUser[i].Rent} (RentCompid:{arrUser[i].RentCompId+1})");
                    }
                    else if (arrUser[i] is Workers)//해당 객체가 Workers 인 경우
                    {
                        Workers tmp = arrUser[i] as Workers;
                        sw.WriteLine($"({i + 1})type:{tmp.Typename}, Name:{arrUser[i].Name}, UserId:{arrUser[i].Uid}, WorkerId:{tmp.Wid}, Used for:{tmp.UseService}, Rent:{arrUser[i].Rent} (RentCompid:{arrUser[i].RentCompId+1})");
                    }
                }  
            }
            sw.WriteLine("===============================================================================");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int comCnt, noteCnt, netCnt, deskCnt, userCnt;
            ComputerManager cm = new ComputerManager();
            ref Computer[] arrComp = ref cm.getComp();
            ref User[] arrUser = ref cm.getUser();
            StreamReader sr = new StreamReader("input.txt");
            StreamWriter sw = new StreamWriter("output.txt");
            string[] arr;
           
            comCnt = int.Parse(sr.ReadLine()); // 총 컴퓨터 수 저장

            arr = sr.ReadLine().Split(" ");
            noteCnt = int.Parse(arr[0]); // 노트북 수 저장
            deskCnt = int.Parse(arr[1]); // 데스크톱 수 저장
            netCnt = int.Parse(arr[2]); // 넷북 수 저장

            arrComp = new Computer[comCnt]; //Computer 객체 배열 생성
            int k=0;
            for (; k < netCnt; k++)
                arrComp[k] = new Netbook(k + 1, k + 1); // 생성해야하는 넷북 개수에 맞게 넷북 객체 생성
            
            for (int j = 0; j < noteCnt; k++, j++)
                arrComp[k] = new Notebook(k + 1, j + 1); // 생성해야하는 노트북 개수에 맞게 노트북 객체 생성
            
            for (int j = 0; j < deskCnt; k++, j++)
                arrComp[k] = new Desktop(k + 1, j + 1); // 생성해야하는 데스크탑 개수에 맞게 데스크탑 객체 생성


            userCnt = int.Parse(sr.ReadLine()); // 유저 수 저장
            arrUser = new User[userCnt]; // User 객체 배열 생성
            for(int i = 0,j=0,p=0,q=0 ; i < userCnt; i++)
            {           
                string[] userInf;
                userInf = sr.ReadLine().Split(" "); // 유저의 타입과 이름 정보를 구해서 userInf에 저장
                if (userInf[0] == "Student") // 유저 타입이 student인 경우
                {
                    arrUser[i] = new Students(userInf[1], i + 1, j + 1); // student 객체 생성
                    j++; // studid 관리
                }
                else if (userInf[0] == "Worker") // 유저 타입이 worker인 경우
                {
                    arrUser[i] = new Workers(userInf[1], i + 1, p + 1); // worker 객체 생성
                    p++;// workerid 관리
                }
                else if (userInf[0] == "Gamer") // 유저 타입이 gamer인 경우
                {
                    arrUser[i] = new Gamers(userInf[1], i + 1, q + 1); // gamer 객체 생성
                    q++;// gamerid 관리
                }
            }
        
            int flag = 0;
            while (flag==0)
            {
                string[] order;
                order = sr.ReadLine().Split(" "); // 명령 한 문장을 띄어쓰기로 분리하여 저장
                switch (order[0])// 명령의 첫 번째 문자열은 처리 명령어이기 때문에 이에 따라 작업을 진행
                {
                    case "Q":
                        flag = 1; // 종료 처리 명령어 Q이기 때문에 flag=1로 만들어주어 전체 반복문을 빠져나와 종료된다.
                        break;
                    case "A": // 사용자를 컴퓨터에 할당하는 작업 수행
                        cm.user_Get_Computer(sw,int.Parse(order[1]),int.Parse(order[2]));
                        break;
                    case "R": // 해당 사용자의 컴퓨터 반납 작업 수행
                        cm.user_Return_Computer(sw,int.Parse(order[1]));
                        break;
                    case "T": // 하루의 시간이 지나가는 작업 수행
                        cm.pass_One_Day(sw);
                        break;
                    case "S": // 현재 상태 표시 작업 수행
                        cm.show_All(sw);               
                        break;
                    default:
                        break;
                }
            }
            sr.Close();
            sw.Close();
        }
    }
}
