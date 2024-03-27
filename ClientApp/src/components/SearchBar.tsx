import SearchIcon from '@mui/icons-material/Search';
import React from "react";

type Props = {
    onClick?: () => void;
    onChange?: React.ChangeEventHandler<HTMLInputElement>
    autoFocus?: boolean;
}

const SearchBar = (props: Props) => {
    return (
        <>
            <div className="searchBar">
                <SearchIcon/>
                <input type="text" placeholder="Search for a business" autoFocus={props.autoFocus} onChange={props.onChange}/>
            </div>

        </>
    )
}

export default SearchBar;