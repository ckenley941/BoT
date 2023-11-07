import "../../App.css";

import React from "react";
import { useNavigate, useLocation } from "react-router-dom";
import Drawer from "@mui/material/Drawer";
import Typography from "@mui/material/Typography";
import Button from '@mui/material/Button';
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import MenuItem from '@mui/material/MenuItem';
import Menu from '@mui/material/Menu';
import PopupState, { bindTrigger, bindMenu } from 'material-ui-popup-state';
import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import {
AddCircleOutlineOutlined,
SubjectOutlined,
PsychologyAlt,
Psychology,
Language,
Chalet,
Settings
} from "@mui/icons-material/";

export default function Layout({ children }) {
let navigate = useNavigate();
let location = useLocation();


/*
 {
    text: "Spanish Word",
    icon: < SubjectOutlined color = "secondary" / > ,
    path: "/word",
  },
  {
    text: "Random Thought",
    icon: < PsychologyAlt color = "secondary" / > ,
    path: "/thought",
  },
 */
const menuItems = [
  {
    text: "Dashboard",
    icon: < Chalet color = "secondary" / > ,
    path: "/",
  },
  {
    text: "Add Thought",
    icon: < AddCircleOutlineOutlined color = "secondary" / > ,
    path: "/add-thought",
  },
  {
    text: "Organize Thoughts",
    icon: < Psychology color = "secondary" / > ,
    path: "/organize-thoughts",
  },
  {
    text: "All Words",
    icon: < Language color = "secondary" / > ,
    path: "/words",
  },
  {
    text: "Settings",
    icon: < Settings color = "secondary" / > ,
    path: "/settings",
  },
];

return (
<div className="layout">
  <AppBar position="fixed" className="appBar" elevation={0} color="primary">
    <Toolbar>
      <Typography>Bucket of Thoughts</Typography>
    </Toolbar>
  </AppBar>

  <Drawer className="drawer" variant="permanent" anchor="left" classes={{ paper: "drawer" }}>
    {/* <div>
      <Typography variant="h5">Dashboard</Typography>
    </div> */}
    <List>
      {menuItems.map((item) => (
      <ListItem key={item.text} onClick={()=> navigate(item.path)}
        className={
        location.pathname === item.path ? "active-drawer" : null
        }
        >
        <ListItemIcon>{item.icon}</ListItemIcon>
        <ListItemText primary={item.text} />
      </ListItem>
      ))}
    </List>    
  </Drawer>
  <PopupState  variant="popover" popupId="demo-popup-menu">
      {(popupState) => (
        <React.Fragment>
          
          <Button className="popover" variant="contained" {...bindTrigger(popupState)}>
            Menu
          </Button>
          <Menu {...bindMenu(popupState)}>
          {menuItems.map((item) => (
            <MenuItem onClick={()=> { navigate(item.path); popupState.close();}}>
              <ListItemIcon>{item.icon}</ListItemIcon>
              <ListItemText primary={item.text} />
            </MenuItem>
          ))}
          </Menu>
        </React.Fragment>
      )}
    </PopupState>
  <div className="page">
    <div className="toolBar">{children}</div>
  </div>
</div>
);
}