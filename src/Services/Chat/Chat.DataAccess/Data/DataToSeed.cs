using Chat.DataAccess.Models;
using Chat.DataAccess.Options;
using Microsoft.Extensions.Options;

namespace Chat.DataAccess.Data
{
    public static class DataToSeed
    {
        public static IEnumerable<ChatRole> GetChatRoles(IOptions<ChatRolesOptions> chatRolesOptions)
            => new List<ChatRole>()
            {
                new ChatRole()
                {
                    Id = new Guid("fb77c354-1e91-47dd-b885-91f89e9a8d91"),
                    Name = chatRolesOptions.Value.User
                },

                new ChatRole()
                {
                    Id = new Guid("f570faec-946a-4c9a-98fe-6bfb63723298"),
                    Name = chatRolesOptions.Value.Admin,
                },

                new ChatRole()
                {
                    Id= new Guid("80807e63-11c7-4d3b-870b-8177aedeb951"),
                    Name = chatRolesOptions.Value.Creator
                }
            };

        public static IEnumerable<MessengerUser> GetMessengerUsers()
            => new List<MessengerUser>()
            {
                new MessengerUser()
                {
                    Id = new Guid("29faa8b5-e805-4268-8f12-56854aad06f1"),
                    FullName = "Padalitsa Maxim",
                    Username = "Unox4321"
                },

                new MessengerUser()
                {
                    Id = new Guid("b4683dd1-7512-48cb-9f34-9a83816290d1"),
                    FullName = "Vorobey Sergey",
                    Username = "Gluk321"
                },

                new MessengerUser()
                {
                    Id = new Guid("a4ad4f83-c8b0-4b43-a75e-36f2e990c6a5"),
                    FullName = "Veronica Stepanova",
                    Username = "Egirl2212"
                },

                new MessengerUser()
                {
                    Id = new Guid("9b11952d-4ed0-45e9-9885-d7ba0d2c8b9b"),
                    FullName = "Global Fedya",
                    Username = "Eboy2224"
                }
            };

        public static IEnumerable<ChatParticipant> GetFirstChatRoomParticipants()
            => new List<ChatParticipant>()
            {
                new ChatParticipant()
                {
                    ChatRoleId = new Guid("fb77c354-1e91-47dd-b885-91f89e9a8d91"),
                    MessengerUserId = new Guid("b4683dd1-7512-48cb-9f34-9a83816290d1"),
                    ChatRoomId = new Guid("2641227c-c818-4dee-945f-d6b04f5692a2"),
                },

                new ChatParticipant()
                {
                    ChatRoleId = new Guid("f570faec-946a-4c9a-98fe-6bfb63723298"),
                    MessengerUserId = new Guid("9b11952d-4ed0-45e9-9885-d7ba0d2c8b9b"),
                    ChatRoomId = new Guid("2641227c-c818-4dee-945f-d6b04f5692a2"),
                },

                new ChatParticipant()
                {
                    ChatRoleId = new Guid("80807e63-11c7-4d3b-870b-8177aedeb951"),
                    MessengerUserId = new Guid("29faa8b5-e805-4268-8f12-56854aad06f1"),
                    ChatRoomId = new Guid("2641227c-c818-4dee-945f-d6b04f5692a2"),
                }
            };

        public static IEnumerable<ChatParticipant> GetSecondChatRoomParticipants()
            => new List<ChatParticipant>()
            {
                new ChatParticipant()
                {
                    ChatRoleId = new Guid("80807e63-11c7-4d3b-870b-8177aedeb951"),
                    MessengerUserId=new Guid("a4ad4f83-c8b0-4b43-a75e-36f2e990c6a5"),
                    ChatRoomId = new Guid("33f774e1-ba87-49b5-8a4c-22021498ca1f")
                },

                new ChatParticipant()
                {
                    ChatRoleId = new Guid("fb77c354-1e91-47dd-b885-91f89e9a8d91"),
                    MessengerUserId = new Guid("9b11952d-4ed0-45e9-9885-d7ba0d2c8b9b"),
                    ChatRoomId = new Guid("33f774e1-ba87-49b5-8a4c-22021498ca1f")
                },

                new ChatParticipant()
                {
                    ChatRoleId = new Guid("fb77c354-1e91-47dd-b885-91f89e9a8d91"),
                    MessengerUserId = new Guid("b4683dd1-7512-48cb-9f34-9a83816290d1"),
                    ChatRoomId = new Guid("33f774e1-ba87-49b5-8a4c-22021498ca1f")
                },

                new ChatParticipant()
                {
                    ChatRoleId = new Guid("fb77c354-1e91-47dd-b885-91f89e9a8d91"),
                    MessengerUserId = new Guid("29faa8b5-e805-4268-8f12-56854aad06f1"),
                    ChatRoomId = new Guid("33f774e1-ba87-49b5-8a4c-22021498ca1f")
                }
            };

        public static IEnumerable<ChatRoom> GetChatRooms()
            => new List<ChatRoom>()
            {
                new ChatRoom()
                {
                    Id = new Guid("2641227c-c818-4dee-945f-d6b04f5692a2"),
                    Title = "The boys",
                    CreatorId = new Guid("29faa8b5-e805-4268-8f12-56854aad06f1"),
                    MessagesCount = 0,
                    MembersNumber = 3
                },

                new ChatRoom()
                {
                    Id = new Guid("33f774e1-ba87-49b5-8a4c-22021498ca1f"),
                    Title = "Simp world",
                    CreatorId = new Guid("a4ad4f83-c8b0-4b43-a75e-36f2e990c6a5"),
                    MessagesCount = 0,
                    MembersNumber = 4
                }
            };

    }
}
