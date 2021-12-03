var LoginForm = React.createClass({
    render: function () {
        return (

            <div className="RegistrationInfo">
                <form className="RegistrationForm">
                    <p><label htmlFor="emailLoginLabel">Email address</label></p>
                    <p><input type="text" id="emailLoginInput" name="emailLogin" /></p>
                    <p><label htmlFor="passwordLoginLabel">Password</label></p>
                    <p><input type="text" id="passwordLoginInput" name="passwordLogin"/></p>
                    <p><input type="submit" defaultValue="Submit" /></p>
                </form>
            </div>
        );
    }
});

React.render(<LoginForm />, document.getElementById('login'));