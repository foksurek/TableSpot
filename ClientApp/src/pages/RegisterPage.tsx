import {useAuth} from "contexts/AuthContext.tsx";
import React, {useState} from "react";
import {useNavigate} from "react-router-dom";
import axios from "axios";
import API_URLS from "ApiConst/ApiUrls.ts";
import {ErrorVariants} from "objects/errorVariants.ts";
import MainAlert from "components/MainAlert.tsx";

const RegisterPage = () => {

    const navigate = useNavigate();
    const { user } = useAuth();
    const [error, setError] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");
    const [errorVariant, setErrorVariant] = useState("info");

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [accountType, setAccountType] = useState(-1);
    
    const handleRegister = async (e: React.FormEvent) => {
        e.preventDefault();

        const fetchData = async () => {
            if (confirmPassword !== password) {
                setError(true);
                setErrorVariant(ErrorVariants.error);
                setErrorMessage("Password dont match.");
                return;
            }
            if (accountType === -1) {
                setError(true);
                setErrorVariant(ErrorVariants.warning);
                setErrorMessage("Please select Account type");
                return;
            }
            await axios.post(API_URLS.AUTHORIZATION.CREATE_ACCOUNT, {
                email: email,
                password: password,
                name: name,
                surname: surname,
                accountTypeId: accountType
            }, {withCredentials: true}).then((response) => {
                if (response.status === 200) {
                    setError(true);
                    setErrorVariant(ErrorVariants.success);
                    setErrorMessage("Account created successfully. Please log in.");
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
        navigate("/")
    }

    return (
        <>

            {error &&
                <MainAlert Variant={errorVariant} Message={errorMessage} setError={setError}/>
            }
            <div className="loginBox">
                <h2>Register Account</h2>
                <form onSubmit={handleRegister} className="loginForm">
                    <label>Email:</label>
                    <input type="email"
                           value={email}
                           onChange={(e) => setEmail(e.target.value)}
                           required
                           maxLength={64}/>
                    <label>Password: </label>
                    <input type="password"
                           value={password}
                           onChange={(e) => setPassword(e.target.value)}
                           required
                           maxLength={64}/>
                    <label>Confirm Password: </label>
                    <input type="password"
                           value={confirmPassword}
                           onChange={(e) => setConfirmPassword(e.target.value)}
                           required
                           maxLength={64}/>
                    <label>Name:</label>
                    <input type="text"
                           value={name}
                           onChange={(e) => setName(e.target.value)}
                           required
                           maxLength={64}/>
                    <label>Surname:</label>
                    <input
                        type="text"
                        value={surname}
                        onChange={(e) => setSurname(e.target.value)}
                        required
                        maxLength={64}/>
                    <label>Account type:</label>
                    <select onChange={(e) => {
                        setAccountType(e.target.value as unknown as number)
                    }} required>
                        <option disabled selected hidden value="-1">Select Account Type</option>
                        <option value="1">User</option>
                        <option value="3">Restaurant Owner</option>

                    </select>
                    <div className="loginFormButtons">
                        <button type="submit">Register</button>
                    </div>
                </form>
            </div>
        </>
    );
}
export default RegisterPage