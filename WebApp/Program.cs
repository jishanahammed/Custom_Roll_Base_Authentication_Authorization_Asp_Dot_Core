using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using na.admin.Models;
using Syncfusion.Licensing;
using WebApp.ConfigService;
using WebApp.Current_User_Info;
using WebApp.Handlers;

var builder = WebApplication.CreateBuilder(args);
SyncfusionLicenseProvider.RegisterLicense("MjE5OTM2NkAzMjMxMmUzMjJlMzNvODlSL1Jxc3BGRXVHWGRIN3EvSVdyZTNCVk1tWG5oclVrKzREV3hGdWw0PQ==;Mgo+DSMBaFt+QHJqVk1mQ1BbdF9AXnNLdFZzT2Jdbz4Nf1dGYl9RQXRaQ1lmTH9Xf0dqUA==;Mgo+DSMBMAY9C3t2VFhiQlJPcEBDQmFJfFBmR2lcelRxcEUmHVdTRHRcQlhhTH9Xc0BiXXZadH0=;Mgo+DSMBPh8sVXJ1S0R+X1pCaV5dX2NLfUN0T2ZcdV90ZDU7a15RRnVfR19mSHpWc0RnUHpWeQ==;MjE5OTM3MEAzMjMxMmUzMjJlMzNDTnBRWThrSk5KcWprUTM1TytTVVlwWWZOUWhrMHFKM0tvdmQzRGU4bERzPQ==;NRAiBiAaIQQuGjN/V0d+Xk9HfVldXHxLflF1VWFTelt6dVBWACFaRnZdQV1mS3tSckBmWHpecHVc;ORg4AjUWIQA/Gnt2VFhiQlJPcEBDQmFJfFBmR2lcelRxcUUmHVdTRHRcQlhhTH9Xc0BiXH9fdX0=;MjE5OTM3M0AzMjMxMmUzMjJlMzNBWVZqZGRmbkR1SE43SmtMQUdkdEwwS0dqcVNGWnkveFNRU3JKY1cvb1gwPQ==;MjE5OTM3NEAzMjMxMmUzMjJlMzNCbkgwckhDOXVKRExLVlFPejNBbm40QWc5ZmJkVGlOcFJYNDdHYWpTcTFrPQ==;MjE5OTM3NUAzMjMxMmUzMjJlMzNBaDBSUDkwbGRPN0Q3cTdtbGo2ZzhsZlgvR1JudUF4WFRlVFJnRkd3bzZJPQ==;MjE5OTM3NkAzMjMxMmUzMjJlMzNXdGdmRG1UbWlSRk1VZi95WDJzS3AxRE1ESlB6dWxkZlpDdkkyRlZDZUpjPQ==;MjE5OTM3N0AzMjMxMmUzMjJlMzNvODlSL1Jxc3BGRXVHWGRIN3EvSVdyZTNCVk1tWG5oclVrKzREV3hGdWw0PQ==");
// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/login/Index");
    options.AccessDeniedPath = new PathString("/login/AccessDenied");
    options.LogoutPath = new PathString("/login/Logout");
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();
builder.Services.AddTransient<ICurrentuserinfo, Currentuserinfo>();
builder.Services.AddTransient<ICheckRolePrivilege, CheckRolePrivilegeService>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areaRoute",
        pattern: "{area:exists}/{controller}/{action}",
        defaults: new { action = "Index" });

    endpoints.MapControllerRoute(
        name: "default",
    pattern: "{controller=login}/{action=Index}/{id?}");


    endpoints.MapRazorPages();
});
app.Run();
