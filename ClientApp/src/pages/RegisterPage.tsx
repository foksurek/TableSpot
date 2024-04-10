import {useAuth} from "ClientApp/src/contexts/AuthContext.tsx";
import React, {useState} from "react";
import {useNavigate} from "react-router-dom";
import axios from "axios";
import API_URLS from "ClientApp/src/ApiConst/ApiUrls.ts";

const RegisterPage = () => {

    const navigate = useNavigate();
    const { user, setUser } = useAuth();
    
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    // const [accountType, setAccountType] = useState(0);
    

    const handleRegister = async (e: React.FormEvent) => {
        e.preventDefault();

        const fetchData = async () => {
            let resp = await axios.post(API_URLS.AUTHORIZATION.CREATE_ACCOUNT, {
                email: email,
                password: password,
                name: name,
                surname: surname,
                accountTypeId: 1
            }, {withCredentials: true});
            if (resp.status === 200) {
                setUser({
                    id: parseInt(resp.data.data.accountId),
                    email: resp.data.data.email
                });
            }
            if (resp.status === 401) {
                alert('Niepoprawne dane logowania');
            }
        }

        fetchData();
    };
    if (user) {
        navigate('/dashboard');
    }
    
    return (
        <div className="loginBox">
            <h2>Logowanie</h2>
            <form onSubmit={handleRegister} className="loginForm">
                <div>
                    <label>
                        Email:
                        <input
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            required
                            maxLength={64}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Password:
                        <input
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                            maxLength={64}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Name:
                        <input
                            type="text"
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                            required
                            maxLength={64}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Surname:
                        <input
                            type="text"
                            value={surname}
                            onChange={(e) => setSurname(e.target.value)}
                            required
                            maxLength={64}
                        />
                    </label>
                </div>
                <div>
                    <label>
                        Account type:
                    </label>
                </div>
                <div className="loginFormButtons">
                    <button type="submit">Zaloguj</button>
                </div>
            </form>
        </div>
    );
}
export default RegisterPage