import {useAuth} from "contexts/AuthContext.tsx";
import React, {useState} from "react";
import {useNavigate} from "react-router-dom";
import axios from "axios";
import API_URLS from "ApiConst/ApiUrls.ts";
import {ErrorVariants} from "objects/errorVariants.ts";
import MainAlert from "components/MainAlert.tsx";

const RegisterPage = () => {

    const navigate = useNavigate();
    const { user, setUser } = useAuth();
    const [error, setError] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");
    const [errorVariant, setErrorVariant] = useState("info");

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [accountType, setAccountType] = useState(1);


    const handleRegister = async (e: React.FormEvent) => {
        e.preventDefault();

        const fetchData = async () => {
            await axios.post(API_URLS.AUTHORIZATION.CREATE_ACCOUNT, {
                email: email,
                password: password,
                name: name,
                surname: surname,
                accountTypeId: accountType
            }, {withCredentials: true}).then((response) => {
                if (response.status === 200) {
                    setUser({
                        id: parseInt(response.data.data.accountId),
                        email: response.data.data.email
                    });
                }
            }).catch((error) => {
                setErrorVariant(ErrorVariants.error)
                setErrorMessage(error.response.data.details[0])
                setError(true);
            });
        }
        
        fetchData();
    };
    if (user) {
        navigate('/dashboard');
    }

    return (
        <>

            {error &&
                <MainAlert Variant={errorVariant} Message={errorMessage}/>
            }
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
                            <select onChange={(e) => {
                                setAccountType(e.target.value as unknown as number)
                            }}>
                                <option value="1">User</option>
                                <option value="3">Restaurant Owner</option>
                            </select>
                        </label>
                    </div>
                    <div className="loginFormButtons">
                        <button type="submit">Zaloguj</button>
                    </div>
                </form>
            </div>
        </>
    );
}
export default RegisterPage