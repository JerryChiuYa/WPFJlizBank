# Welcome to Jlizbank App
## Purpose

This is a simulate internet banking App, which uses WPF.
Here, you can create your own account to transfer money, view the balance, transaction details and so on.
<p></p>
<img src="/ImgForIntro/Welcome.png"/>

<p></p>

<img src="/ImgForIntro/WPF.gif"/>
<hr>



### Built with
<ul>
	<li><a href='https://visualstudio.microsoft.com/zh-hant/vs/'>Visual Studio 2022</a></li>
	<li><a href='https://docs.microsoft.com/zh-tw/dotnet/desktop/wpf/getting-started/?view=netframeworkdesktop-4.8'>WPF .NET Framework 4.8</a></li>

</ul>

### NuGet Package
<ul>
	<li><a href='https://github.com/XamlFlair/XamlFlair'>XamlFlair</a></li>
	<li><a href='https://github.com/dotnet/reactive'>System.Reactive (It has dependency with XamlFlair)</a></li>
	<li><a href='https://www.nuget.org/packages/Microsoft.Extensions.Logging.Abstractions/6.0.1'>Microsoft.Extensions.Logging.Abstractions (It has dependency with XamlFlair)</a></li>
</ul>

### Database & Tools
<ul>
    <li><a href='https://docs.microsoft.com/zh-tw/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16'>SSMS</a></li>
	<li><a href='https://docs.microsoft.com/zh-tw/dotnet/framework/data/adonet/ado-net-overview'>ADO .NET</a></li>

</ul>

### Image Sources
<ul>
	<li><a href='https://pixabay.com/'>Pixbay</a></li>
</ul>

## Get Started
### 1.Prepare the data

<ul>
<li>Use SSMS</li>
<p>Please copy my variable name and data type to let C# easily to use.</p>
<img src="/ImgForIntro/ssms.png" width="50%" height="50%"/>
</ul>


<ul>
<li>In App.config</li>
</ul>

```xml
<configuration>
<!--In the tag of configuration, add tag <connectionStrings>-->
	<connectionStrings>
		<add name="JlizBank" connectionString="Data Source=localhost;Initial Catalog=JlizBank;Persist Security Info=False;User ID=JC;Password=38405200;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;" providerName="System.Data.SqlClient" />
	</connectionStrings>

</configuration>
```

<ul>
<li>If we want to connect SQL to get data, any below code to grab the connection strings</li>
</ul>


```CSharp
//import namespace
using System.Configuration;
//................................
private string _dbConnStr = ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString;
```

<ul>
<li>Use ADO.NET if we want to grab data in database</li>
</ul>

```CSharp
//Take the CustomerService.cs for example
//import namespace
using System.Data.SqlClient;
//.....................................................................................
public BankAccount GetBankAccount(string AccountNum)
        {
            using (var conn = new SqlConnection(_dbConnStr))
            {
                var account = new BankAccount();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
				
				//write T-SQL Command
				
                cmd.CommandText = @"select * from BankAccount where AccountNum=@AccountNum";

                cmd.Parameters.AddWithValue("@AccountNum", AccountNum);
                cmd.Connection.Open();
                var dr = cmd.ExecuteReader();
			}
		}
```

<hr>

### 2.Register
<ul>
<li>Add a custom validation class to use Regex, which is extends from the class of ValidationRule</li>
</ul>

```CSharp
//import namespace
using System.Text.RegularExpressions;
using System.Windows.Controls;
using ValidationResult = System.Windows.Controls.ValidationResult;

namespace BankLibrary.Services
{
    public class ValidationCheck : ValidationRule
    {
        public string CheckType { get; set; }
        public string ErrorMessage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var settings = new SettingsService(ConfigurationManager.ConnectionStrings["JlizBank"].ConnectionString);
            switch (CheckType)
            {

                case "IdentityType":
                    if (Regex.IsMatch(value.ToString(), @"^[A-Z]{1}[12]{1}\d{8}$")==false)
                    {
                        ErrorMessage = "身分證格式錯誤!!";
                        return new ValidationResult(false, ErrorMessage);
                    }
                    break;

                case "MobileType":
                    if (Regex.IsMatch(value.ToString(), @"^09\d{8}$")==false)
                    {
                        ErrorMessage = "手機格式錯誤,請以09開頭";
                        return new ValidationResult(false, ErrorMessage);
                    }
                    break;

                case "EmailType":
                    if (new EmailAddressAttribute().IsValid(value.ToString())==false)
                    {
                        ErrorMessage = "不是有效的Email格式!!";
                        return new ValidationResult(false, ErrorMessage);
                    }
                    break;

                case "LoginAccountType":
                    var data=settings.GetBankAllData(value.ToString());
                    if (data.Count==1)
                    {
                        ErrorMessage = "此帳號已存在,請重新輸入!!";
                        return new ValidationResult(false, ErrorMessage);
                    }
                    break;
            }
            return new ValidationResult(true, null);
        }
    }
}
```

<ul>
<li>In xaml, add a Template to custom error message</li>
</ul>

```Xaml
<!--import namespace of what we had created--ValidationCheck-->
<Window 
	xmlns:vc="clr-namespace:BankLibrary.Services;assembly=BankLibrary"
/>

<!--add a ValidateTemplate to show error message-->
<Window.Resources>
	<ControlTemplate x:Key="ValidateTemplate">
		<DockPanel>
			<TextBlock DockPanel.Dock="Bottom" Foreground="Red" Text="{Binding ElementName=myAdorn, Path=AdornedElement.(Validation.Errors)[0].ErrorContent}" FontSize="13">
			</TextBlock>
			<Border BorderBrush="Red" BorderThickness="2" CornerRadius="5">
				<AdornedElementPlaceholder x:Name="myAdorn"></AdornedElementPlaceholder>
			</Border>
		</DockPanel>
	</ControlTemplate>
</Window.Resources>

<TextBox  Validation.ErrorTemplate="{StaticResource ValidateTemplate}" Validation.Error="Check_Error">
	<TextBox.Text>
		<Binding Path="UserName" UpdateSourceTrigger="LostFocus" NotifyOnValidationError="True">
			<Binding.ValidationRules >
				<vc:ValidationCheck CheckType="NameType" ></vc:ValidationCheck>
			</Binding.ValidationRules>
		</Binding>
	</TextBox.Text>
</TextBox>
```

<ul>
<li>In C#, make button to be disabled if any invalid error occurs</li>
</ul>

```CSharp
private int _coountError;
	private void Check_Error(object sender, ValidationErrorEventArgs e)
	{
		if (e.Action == ValidationErrorEventAction.Added)
		{
			_coountError++;
		}
		else
		{
			_coountError--;
		}
		this.SaveData.IsEnabled = _coountError > 0 ? false : true;
	}
```

#### Here is the result
<img src="/ImgForIntro/register.png"/>

<hr>

### Other functions
<ul>

<li>Modify Personal Information</li>
<img src="/ImgForIntro/modify.png"/>


</ul>