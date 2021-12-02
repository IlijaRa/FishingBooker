var RegisterForm = React.createClass({
    render: function () {
        return (
            <form action="/action_page.php">
                <div className="RegistrationInfo">
                    <div>
                                <div className="LeftPartInfo">
                                    <p><label htmlFor="fnameLabel">First Name</label></p>
                                    <p><input type="text" id="fnameInput" name="firstname" /></p>

                                    <p><label htmlFor="lnameLabel">Last Name</label></p>
                                    <p><input type="text" id="lnameInput" name="lastname" /></p>

                                    <p><label htmlFor="phoneLabel">Phone number</label></p>
                                    <p><input type="text" id="phoneInput" name="phonenumber" /></p>

                                    <p><label htmlFor="emailLabel">Email address</label></p>
                                    <p><input type="text" id="emailInput" name="emailaddress" /></p>
                                </div>
                                <div className="RightPartInfo">
                                    <p><label htmlFor="passwordLabel1">Password</label></p>
                                    <p><input type="text" id="passwordInput1" name="password1" /></p>

                                    <p><label htmlFor="passwordLabel2">Type password again</label></p>
                                    <p><input type="text" id="passwordInput2" name="password2" /></p>

                                    <p><label htmlFor="addressLabel">Address</label></p>
                                    <p><input type="text" id="addressInput" name="address" /></p>

                                    <p><label htmlFor="cityLabel">City</label></p>
                                    <p><input type="text" id="cityInput" name="city" /></p>
                                    
                                </div>

                                <div className="SubmitButton">
                                    <p><input type="submit" defaultValue="Submit" /></p>
                                </div>
                    </div>
                    
                    
                </div>
            </form>
        );
    }
});

React.render(<RegisterForm />, document.getElementById('register'));