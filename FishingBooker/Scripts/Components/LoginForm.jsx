var LoginForm = React.createClass({
    render: function () {
        return (

            <div className="RegistrationInfo">
                <form className="RegistrationForm">
                    <p><label htmlFor="fname">First Name</label></p>
                    <p><input type="text" id="fname" name="firstname" /></p>
                    <p><label htmlFor="lname">Last Name</label></p>
                    <p><input type="text" id="lname" name="lastname"/></p>
                    <p><input type="submit" defaultValue="Submit" /></p>
                </form>
            </div>
        );
    }
});

React.render(<LoginForm />, document.getElementById('login'));