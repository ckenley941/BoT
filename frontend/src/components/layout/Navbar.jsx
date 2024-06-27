import React, { useState } from "react";
import { makeStyles } from "@mui/material/styles";
import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import MenuIcon from "@mui/material/Menu";
import Button from "@mui/material/Button";
import useMediaQuery from "@mui/material/useMediaQuery";
 
import {
    List,
    ListItem,
    ListItemText,
    Collapse,
} from "@mui/material";
import ExpandLess from "@mui/icons-material/ExpandLess";
import ExpandMore from "@mui/icons-material/ExpandMore";
 
// Exporting Default Navbar to the App.js File
export default function Navbar() {
    const small = useMediaQuery("(max-width:600px)");
    const full = useMediaQuery("(min-width:600px)");
 
    const [open, setOpen] = useState(false);
 
    const handleClick = () => {
        setOpen(!open);
    };
 
    return (
        <div >
            <AppBar position="static">
                <Toolbar variant="dense">
                    {small && (
                        <>
                            <List>
                                <ListItem button>
                                    <Button
                                        onClick={
                                            handleClick
                                        }
                                    >
                                        <MenuIcon />
                                        {open ? (
                                            <ExpandLess />
                                        ) : (
                                            <ExpandMore />
                                        )}
                                    </Button>
                                    <Typography
                                        variant="h6"
                                        color="inherit"
                                        onClick={() => {
                                            console.log(
                                                "logo clicked"
                                            );
                                            setOpen(false);
                                        }}
                                    >
                                        Geeks for Geeks
                                    </Typography>
                                </ListItem>
                                <Collapse
                                    in={open}
                                    timeout="auto"
                                    unmountOnExit
                                >
                                    <List
                                        component="div"
                                        disablePadding
                                    >
                                        <ListItem button>
                                            <ListItemText primary="Home" />
                                        </ListItem>
                                        <ListItem button>
                                            <ListItemText primary="About" />
                                        </ListItem>{" "}
                                        <ListItem button>
                                            <ListItemText primary="Contact" />
                                        </ListItem>
                                    </List>
                                </Collapse>
                            </List>
                        </>
                    )}
 
                    {full && (
                        <>
                            <Typography
                                variant="h6"
                                color="inherit"
                            >
                                Geeks for Geeks
                            </Typography>
                            <Button color="inherit">
                                Home
                            </Button>
 
                            <Button color="inherit">
                                About
                            </Button>
                            <Button color="inherit">
                                Contact
                            </Button>
                        </>
                    )}
                </Toolbar>
            </AppBar>
        </div>
    );
}