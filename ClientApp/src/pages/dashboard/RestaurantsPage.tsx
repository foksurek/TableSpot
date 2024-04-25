import SideNavigation from "components/pageElements/SideNavigation.tsx";
import React, {useEffect, useState } from "react";
import {ACCOUNT__GET_MY_RESTAURANTS, CATEGORY__GET_ALL} from "ApiConst/ApiResponse.ts";
import BusinessCard from "components/BusinessCard.tsx";
import axios from "axios";
import API_URLS from "ApiConst/ApiUrls.ts";
import DivButton from "components/divButton.tsx";
import {Modal} from "@mui/material";
import MainAlert from "components/MainAlert.tsx";
import {ErrorVariants} from "objects/errorVariants.ts";

const RestaurantsPage = () => {

    const [restaurants, setRestaurants] = useState<ACCOUNT__GET_MY_RESTAURANTS | undefined>();
    const [categories, setCategories] = useState<CATEGORY__GET_ALL | undefined>();
    const [createModalOpen, setCreateModalOpen] = useState<boolean>(false);
    
    
    const [name, setName] = useState<string | undefined>();
    const [category, setCategory] = useState<number>(-1);
    const [address, setAddress] = useState<string | undefined>();
    const [description, setDescription] = useState<string | undefined>();
    const [imageUrl, setImageUrl] = useState<string | undefined>();
    const [phoneNumber, setPhoneNumber] = useState<string | undefined>();
    const [email, setEmail] = useState<string | undefined>();

    const [error, setError] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");
    const [errorVariant, setErrorVariant] = useState("info");
    
    const [isRequesting, setIsRequesting] = useState(false);
    
    const createModelHandler = () => {
        setCreateModalOpen(!createModalOpen);
    }

    const fetchData = async () => {
        await axios.get(API_URLS.ACCOUNT.GET_MY_RESTAURANTS, {withCredentials: true})
            .then((response) => setRestaurants(response.data));
        
        await axios.get(API_URLS.CATEGORY.GET_ALL, {withCredentials: true})
            .then((response) => setCategories(response.data));
    }
    
    const createRestaurant = async (e: React.FormEvent) => {
        setIsRequesting(true);
        e.preventDefault();
        await axios.post(API_URLS.RESTAURANT.CREATE, {
            name: name,
            categoryId: category,
            address: address,
            description: description,
            imageUrl: imageUrl,
            phoneNumber: phoneNumber,
            email: email
        }, {withCredentials: true})
            .then(() => {
                fetchData();
                createModelHandler();
                setError(true);
                setErrorVariant(ErrorVariants.success);
                setErrorMessage(`Restaurant ${name} created successfully.`);
            })
            .catch((error) => {
                setError(true);
                setErrorVariant(ErrorVariants.error);
                setErrorMessage(error.response.data.details[0]);
            }).finally(() => {
                setIsRequesting(false);
            });
    }

    // eslint-disable-next-line react-hooks/rules-of-hooks
    useEffect(() => {
        fetchData();
    }, []);


    return (
        <>
        {error && <MainAlert Variant={errorVariant} Message={errorMessage} setError={setError}/>}
            <Modal open={createModalOpen} onClose={createModelHandler}>
                <div className="addBusinessModal">
                    <h1>Create a new business</h1>
                    <form onSubmit={createRestaurant}>
                        <label>Name</label>
                        <input required type="text" placeholder="Business name" value={name} onChange={(e) => {
                            setName(e.target.value)
                        }}/>
                        <label>Category</label>
                        <select required value={category} onChange={(e) => {
                            setCategory(parseInt(e.target.value))
                        }}>
                            {/*TODO: Create endpoint to get categories and get them form it*/}
                            {categories && <option value={-1}>Select a category</option>}
                            {!categories && <option value={-1}>Can't fetch categories. Try again later</option>}
                            {
                                categories?.data.map((category) => {
                                    return (
                                        <option key={category.id} value={category.id}>{category.name}</option>
                                    );
                                })
                            }
                        </select>
                        <label>Address</label>
                        <input required type="text" placeholder="Business address" value={address} onChange={(e) => {
                            setAddress(e.target.value)
                        }}/>
                        <label>Description</label>
                        <textarea required placeholder="Business description" value={description} onChange={(e) => {
                            setDescription(e.target.value)
                        }}/>
                        {/*TODO: Add file upload system*/}
                        <label>Image</label>
                        <input required type="text" placeholder="Business image url" value={imageUrl} onChange={(e) => {
                            setImageUrl(e.target.value)
                        }}/>
                        <h3>Optional</h3>
                        <label>Phone number</label>
                        <input type="text" placeholder="Business phone number" value={phoneNumber} onChange={(e) => {
                            setPhoneNumber(e.target.value)
                        }}/>
                        <label>Email</label>
                        <input type="text" placeholder="Business email" value={email} onChange={(e) => {
                            setEmail(e.target.value)
                        }}/>
                        <h3></h3>
                        {!isRequesting ? <DivButton isButton={true}>Create</DivButton> :
                            <DivButton isDisabled={true} isButton={true}>Creating...</DivButton>}
                    </form>
                </div>
            </Modal>
            <SideNavigation/>
            <main className="DashboardRestaurantsContainer">
                <div className="header">
                    <h1>My businesses</h1>
                    <DivButton onClick={createModelHandler}>Add New Business</DivButton>
                </div>
                <div className="content">
                    {restaurants?.data.map((restaurant) => {
                        return (
                            <BusinessCard restaurant={restaurant} key={restaurant.id}/>
                        );
                    })}
                </div>
            </main>
        </>
    )
}
export default RestaurantsPage;