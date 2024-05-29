

function registerCompany() {
	var defaultBtnValue = $('#submit_btn').html();
	$('#submit_btn').html("Please wait...");
	$('#submit_btn').attr("disabled", true);
	var data = {};
	var CompanyLogo = document.getElementById("companyLogo").files;
	data.Name = $('#companyName').val();
	data.Email = $('#email').val();
	data.CompanyAddress = $('#companyAddress').val();
	data.Address = $('#address').val();
	data.Phone = $('#phoneNumber').val();
	data.Mobile = $('#mobileNumber').val();
	data.FirstName = $('#firstName').val();
	data.LastName = $('#lastName').val();
	data.Password = $('#password').val();
	data.Referral = $('#referral').val();
	data.CheckBox = $('#termsCondition').is(":checked");
	data.IsAdmin = true;
	data.ConfirmPassword = $('#confirmpassword').val();
	const reader = new FileReader();

	var base64;
	if (CompanyLogo.length > 0) {
		reader.readAsDataURL(CompanyLogo[0]);
		reader.onload = function () {
			base64 = reader.result;
			if (data.Phone.length == 11 || data.Phone.length == 13) {
				if (data.Mobile.length == 11 || data.Mobile.length == 13 || data.Mobile == "") {
					var companyDetails = JSON.stringify(data);
					if (data.CheckBox != "") {
						$.ajax({
							type: 'Post',
							url: '/Account/CompanyRegistration', // we are calling json method
							dataType: 'json',
							data:
							{
								companyDetails: companyDetails,
								base64: base64
							},
							success: function (result) {
								if (!result.isError) {
									var url = '/Account/Login';
									successAlertWithRedirect(result.msg, url);
									$('#submit_btn').html(defaultBtnValue);
								}
								else {
									$('#submit_btn').html(defaultBtnValue);
									$('#submit_btn').attr("disabled", false);
									errorAlert(result.msg);
								}
							},
							error: function (ex) {
								$('#submit_btn').html(defaultBtnValue);
								$('#submit_btn').attr("disabled", false);
								errorAlert("An error occured, please try again. Please contact admin if issue persists.");
							}
						});
					} else {
						$('#submit_btn').html(defaultBtnValue);
						$('#submit_btn').attr("disabled", false);
						errorAlert("I have read and agree to the Terms and Conditions to Continue Thanks!.");
					}
				}
				else {
					$('#submit_btn').html(defaultBtnValue);
					$('#submit_btn').attr("disabled", false);
					errorAlert("Mobile Number must be equal to 11 or 13 digit");
				}
			}
			else {
				$('#submit_btn').html(defaultBtnValue);
				$('#submit_btn').attr("disabled", false);
				errorAlert("Phone Number must be equal to 11 or 13 digit");
			}
		}
	} else {
		if (data.Phone.length == 11 || data.Phone.length == 13) {
			if (data.Mobile.length == 11 || data.Mobile.length == 13 || data.Mobile == "") {
				var companyDetails = JSON.stringify(data);
				if (data.CheckBox != "") {
					$.ajax({
						type: 'Post',
						url: '/Account/CompanyRegistration', // we are calling json method
						dataType: 'json',
						data:
						{
							companyDetails: companyDetails,
							base64: base64
						},
						success: function (result) {
							if (!result.isError) {
								var url = '/Account/Login';
								successAlertWithRedirect(result.msg, url);
								$('#submit_btn').html(defaultBtnValue);
							}
							else {
								$('#submit_btn').html(defaultBtnValue);
								$('#submit_btn').attr("disabled", false);
								errorAlert(result.msg);
							}
						},
						error: function (ex) {
							$('#submit_btn').html(defaultBtnValue);
							$('#submit_btn').attr("disabled", false);
							errorAlert("An error occured, please try again. Please contact admin if issue persists.");
						}
					});
				} else {
					$('#submit_btn').html(defaultBtnValue);
					$('#submit_btn').attr("disabled", false);
					errorAlert("I have read and agree to the Terms and Conditions to Continue Thanks!.");
				}
			}
			else {
				$('#submit_btn').html(defaultBtnValue);
				$('#submit_btn').attr("disabled", false);
				errorAlert("Mobile Number must be equal to 11 or 13 digit");
			}
		}
		else {
			$('#submit_btn').html(defaultBtnValue);
			$('#submit_btn').attr("disabled", false);
			errorAlert("Phone Number must be equal to 11 or 13 digit");
		}
	}
}

function CreateCompany() {
	var defaultBtnValue = $('#submit_btn').html();
	$('#submit_btn').html("Please wait...");
	$('#submit_btn').attr("disabled", true);
	
	var data = {};
	data.CompanyName = $('#organizationName').val();
	data.Email = $('#companyEmail').val();
	data.Address = $('#companyAddress').val();
	data.CompanyPhone = $('#companyPhone').val();
	data.FirstName = $('#fname').val();
	data.LastName = $('#lname').val();
	data.Address = $('#companyAddress').val();
	data.StaffPosition = $('#staffPosition').val();
	data.Password = $('#password').val();
	data.ConfirmPassword = $('#confirmPassword').val();
	var base64 = document.getElementById("companyLogo").files;
	
	if (data.CompanyName != "" && data.Email != "" && data.Address != "" && data.CompanyPhone != "" &&
		data.FirstName != "" && data.LastName != "" && data.StaffPosition != "" && data.Password != "" && data.ConfirmPassword != "") {
		if (base64[0] != null) {
			const reader = new FileReader();
			reader.readAsDataURL(base64[0]);
			reader.onload = function () {
				base64 = reader.result;
				let companyDetails = JSON.stringify(data);
				$.ajax({
					type: 'Post',
					url: '/Account/CompanyRegistration',
					dataType: 'json',
					data:
					{
						companyDetails: companyDetails,
						base64: base64
					},
					success: function (result) {
						if (!result.isError) {
							var url = '/Account/Login';
							successAlertWithRedirect(result.msg, url);
						}
						else {
							errorAlert(result.msg);
						}
					},
					error: function (ex) {
						errorAlert("Please check and try again. Contact Admin if issue persists..");
					}
				});
			}
		} else {
			let companyDetails = JSON.stringify(data);
			$.ajax({
				type: 'Post',
				url: '/Account/CompanyRegistration',
				dataType: 'json',
				data:
				{
					companyDetails: companyDetails,
				},
				success: function (result) {
					if (!result.isError) {
						var url = '/Account/Login';
						successAlertWithRedirect(result.msg, url);
					}
					else {
						errorAlert(result.msg);
					}
				},
				error: function (ex) {
					errorAlert("Please check and try again. Contact Admin if issue persists..");
				}
			});
		}
	}
	else {
		errorAlert("Please fill the form Correctly");
	}
}

function login() {
	var defaultBtnValue = $('#submit_btn').html();
	$('#submit_btn').html("Please wait...");
	$('#submit_btn').attr("disabled", true);

	var email = $('#email').val();
	var password = $('#password').val();
	if (email != "" && password != "") {
		$.ajax({
			type: 'Post',
			url: '/Account/Login',
			dataType: 'json',
			data:
			{
				email: email,
				password: password
			},
			success: function (result) {
				if (!result.isError) {
					var n = 1;
					localStorage.removeItem("on_load_counter");
					localStorage.setItem("on_load_counter", n);
					location.replace(result.dashboard);
					return;
				}
				else {
					errorAlert(result.msg);
				}
			},
			error: function (ex) {
				$('#submit_btn').html(defaultBtnValue);
				$('#submit_btn').attr("disabled", false);
				errorAlert("An error occured, please try again.");
			}
		});
	}
    else {
		errorAlert("Please fill the form Correctly");
    }
	
}