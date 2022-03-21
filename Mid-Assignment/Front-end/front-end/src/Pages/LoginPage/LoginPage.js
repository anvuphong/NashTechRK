import axios from "axios";
import React, { useEffect, useState } from "react";
import { Button, Form } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const validateEmail = (email) => {
    if (!email) return 'Required!';
    const isvalidEmail = String(email)
        .toLowerCase()
        .match(
            /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
        );
    if (!isvalidEmail) return 'Invalid Email'
    return '';
};

const validatePassword = password => {
    if (!password) return 'Required!';
    if (password.length < 8) return 'At least 8 character';
    return '';
}

const LoginPage = () => {
    const [value, setValue] = useState({
        email: '',
        password: ''
    });
    const [touched, setTouched] = useState({
        email: false,
        password: false
    });
    const error = {
        email: validateEmail(value.email),
        password: validatePassword(value.password)
    };
    const [isLogin, setIslogin] = useState(false);
    const navigatge = useNavigate();

    useEffect(() => {
        if (isLogin) {
            let isCancel = false;
            axios({
                method: 'GET',
                url: 'https://60dff0ba6b689e001788c858.mockapi.io/token'
            }).then(response => {
                if (!isCancel) {
                    localStorage.setItem('token', response.data.token);
                    localStorage.setItem('userId', response.data.userId);
                    navigatge('/');
                    window.location.reload();
                }
            });
            return () => {
                isCancel = true;
            }
        }
    }, [isLogin]);


    const handleInputChange = (evt) => {
        setValue({
            ...value,
            [evt.target.name]: evt.target.value
        })
    };
    const handleOnBlur = evt => {
        setTouched({
            ...value,
            [evt.target.name]: true
        })
    };
    const isValidForm = error.email || error.password;
    const handleSubmit = evt => {
        evt.preventDefault();
        setIslogin(true);
    };

    return (
        <div >
            <p style={{ marginLeft: '50px' }}>You need to enter Username and Password to login</p>
            <Form style={{ width: '50%', marginLeft: '50px' }}>
                <Form.Group className="add" id="inputUsername">
                    <Form.Label>Username</Form.Label>
                    <Form.Control
                        name="username"
                        type="text"
                        onChange={handleInputChange}
                        onBlur={handleOnBlur}
                    // value={data.name}
                    />
                    <Form.Text muted>
                        Enter Username.
                    </Form.Text>
                </Form.Group>

                <Form.Group className="add" id="inputPassword">
                    <Form.Label>Password</Form.Label>
                    <Form.Control
                        name="password"
                        type="text"
                        onChange={handleInputChange}
                        onBlur={handleOnBlur}
                    // value={data.name}
                    />
                    <Form.Text muted>
                        Enter Password.
                    </Form.Text>
                </Form.Group>
                <Button style={{ marginTop: '20px' }} variant="outline-primary" type="submit">Login</Button>
            </Form>
        </div>
    )
}
export default LoginPage;