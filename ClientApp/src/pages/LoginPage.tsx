import React, { useState } from 'react';
import {useAuth} from "../contexts/AuthContext.tsx";
import axios from "axios";

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

    const handleLogin = async (e: React.FormEvent) => {
        e.preventDefault();
        
        const fetchData = async () => {
            let resp = await axios.post<ApiResponse>('http://localhost:5115/api/Auth/Login', {
                email,
                password
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
        return (
            <div>
                <h2>Witaj, {user.email}</h2>
            </div>
        );
    }
    

    return (
        <div className="loginBox">
            <h2>Logowanie</h2>
            <form onSubmit={handleLogin} className="loginForm">
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
                        Hasło:
                        <input
                            type="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            required
                            maxLength={64}
                        />
                    </label>
                </div>
                <button type="submit">Zaloguj</button>
            </form>
        </div>
        // <div>
        //     <h2>Logowanie</h2>
        //     <form onSubmit={handleLogin}>
        //         <div>
        //             <label>
        //                 Email:
        //                 <input
        //                     type="email"
        //                     value={email}
        //                     onChange={(e) => setEmail(e.target.value)}
        //                     required
        //                 />
        //             </label>
        //         </div>
        //         <div>
        //             <label>
        //                 Hasło:
        //                 <input
        //                     type="password"
        //                     value={password}
        //                     onChange={(e) => setPassword(e.target.value)}
        //                     required
        //                 />
        //             </label>
        //         </div>
        //         <button type="submit">Zaloguj</button>
        //     </form>
        // </div>
    );
};

export default LoginPage;
