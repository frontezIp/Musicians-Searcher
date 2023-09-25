using Chat.DataAccess.Contexts;
using Chat.DataAccess.Data;
using Chat.DataAccess.Options;
using Microsoft.Extensions.Options;

namespace Chat.API.Extensions
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ChatContext>();

            var chatRolesOptions = scope.ServiceProvider.GetRequiredService<IOptions<ChatRolesOptions>>();

            if (!context.ChatRoles.Any())
            {
                await context.ChatRoles.AddRangeAsync(DataToSeed.GetChatRoles(chatRolesOptions));
                await context.SaveChangesAsync();
            }

            if (!context.MessangerUsers.Any())
            {
                await context.MessangerUsers.AddRangeAsync(DataToSeed.GetMessengerUsers());
                await context.SaveChangesAsync();
            }

            if (!context.ChatRooms.Any())
            {
                await context.ChatRooms.AddRangeAsync(DataToSeed.GetChatRooms());
                await context.SaveChangesAsync();
            }

            if (!context.ChatParticipants.Any())
            {
                await context.ChatParticipants.AddRangeAsync(DataToSeed.GetFirstChatRoomParticipants());
                await context.ChatParticipants.AddRangeAsync(DataToSeed.GetSecondChatRoomParticipants());
                await context.SaveChangesAsync();
            }
        }
    }
}
