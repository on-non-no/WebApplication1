using TestAPP.Data;

namespace TestAPP
{
    public static class DataSeeder
    {
        public static void Seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<TestDataContext>();
            context.Database.EnsureCreated();
            AddTest(context);
        }

        private static void AddTest(TestDataContext context)
        {
            var 사용자 = context.사용자들.FirstOrDefault();
            if (사용자 != null) return;

            context.사용자들.Add(new 사용자
            {
                아이디 = "choi",
                비밀번호 = "62461400",
                이메일 = "choi@wise.co.kr",
                이름 = "최광열",
                소속 = "위세아이텍",
                생년월일 = "240222",
                전화번호 = "010-2745-1802",
                계정권한들 = new List<계정권한>
                {
                    new 계정권한 { 권한분류 = "관리자"},
                }
            });

            context.사용자들.Add(new 사용자
            {
                아이디 = "lee",
                비밀번호 = "62461400",
                이메일 = "lee@wise.co.kr",
                이름 = "이종찬",
                소속 = "위세아이텍",
                생년월일 = "19960911",
                전화번호 = "01071651146",
                계정권한들 = new List<계정권한>
                {
                    new 계정권한 { 권한분류 = "개발자"},
                }
            });

            context.사용자들.Add(new 사용자
            {
                아이디 = "bjw",
                비밀번호 = "62461400",
                이메일 = "bjw@wise.co.kr",
                이름 = "변정원",
                소속 = "위세아이텍",
                생년월일 = "000000",
                전화번호 = "010-1234-5678",
                계정권한들 = new List<계정권한>
                {
                    new 계정권한 { 권한분류 = "연구자"},
                }
            });

            context.SaveChanges();
        }
    }
}
