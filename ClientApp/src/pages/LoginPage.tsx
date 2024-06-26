﻿import React, { useState } from 'react';
import {useAuth} from "contexts/AuthContext.tsx";
import axios from "axios";
import API_URLS from "ApiConst/ApiUrls.ts";
import {Link, useNavigate} from "react-router-dom";
import MainAlert from "components/MainAlert.tsx";

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
    const navigate = useNavigate();
    const { user, setUser } = useAuth();
    
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const [error, setError] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");
    const [errorVariant, setErrorVariant] = useState("info");


    if (user) {
        navigate("/")
    }
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

    return (
        <>
            {error &&
                <MainAlert Variant={errorVariant} Message={errorMessage} setError={setError}/>
            }
            <div className="login-box">
                <h2>Login</h2>
                <form onSubmit={handleLogin} className="login-form">
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
                    <div className="login-form-buttons">
                        <button type="submit">Login</button>
                        <span>Dont have account yet? <Link className="blue-link" to="/register">REGISTER NOW</Link></span>
                    </div>
                </form>
            </div>
        </>
    );
};

export default LoginPage;
