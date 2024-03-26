import SearchIcon from '@mui/icons-material/Search';

type Props = {
    onClick?: () => void;
    onChange?: () => void;
}

const SearchBar = (props: Props) => {
    return (
        <>
            <div className="searchBar" onClick={props.onClick}>
                <SearchIcon/>
                <input type="text" onChange={props.onChange} placeholder="Search for a business"/>
            </div>
        </>
    )
}

export default SearchBar;