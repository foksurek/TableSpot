import React, { useState } from 'react';
import {useAuth} from "../contexts/AuthContext.tsx";
import axios from "axios";
import API_URLS from "../ApiConst/ApiUrls.ts";
import {Link} from "react-router-dom";
import MainAlert from "../components/MainAlert.tsx";

type ApiResponse = {
    code: number;
    data: {
        email: string;
        accountId: string;
        accountType: {
            id: string;
            type: string;
        }
    }
}


const LoginPage = () => {
    const { user, setUser } = useAuth();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const [error, setError] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");
    const [errorVariant, setErrorVariant] = useState("info");


    const handleLogin = async (e: React.FormEvent) => {
        e.preventDefault();

        const fetchData = async () => {
            await axios.post<ApiResponse>(API_URLS.AUTHORIZATION.LOGIN, {
                email,
                password
            }, {withCredentials: true})
                .then(response => {
                    setUser({
                        id: parseInt(response.data.data.accountId),
                        email: response.data.data.email,
                        accountType: parseInt(response.data.data.accountType.id)
                    });
                })
                .catch(error => {
                    setError(true);
                    setErrorVariant("error");
                    setErrorMessage(error.response.data.details[0]);
                });
        }

        fetchData();
    };

    if (user) {
        return (
            <div>
                {/*TODO: Change to name ands surname*/}
                <h2>Hello, {user.email}</h2>
            </div>
        );
    }


    return (
        <>
            {error &&
                <MainAlert Variant={errorVariant} Message={errorMessage}/>
            }
            <div className="loginBox">
                <h2>Login</h2>
                <form onSubmit={handleLogin} className="loginForm">
                    <label>Email:</label>
                    <input type="email"
                           value={email}
                           onChange={(e) => setEmail(e.target.value)}
                           required
                           maxLength={64}/>
                    <label>Password:</label>
                    <input type="password"
                           value={password}
                           onChange={(e) => setPassword(e.target.value)}
                           required
                           maxLength={64}/>
                    <div className="loginFormButtons">
                        <button type="submit">Zaloguj</button>
                        <span>Dont have account yet? <Link className="blueLink" to="/register">REGISTER NOW</Link></span>
                    </div>
                </form>
            </div>
        </>
    );
};

export default LoginPage;
