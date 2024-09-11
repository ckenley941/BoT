import "../../App.css";

import React from "react";
import { useNavigate, useLocation } from "react-router-dom";
import Drawer from "@mui/material/Drawer";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import List from "@mui/material/List";
import ListItem from "@mui/material/ListItem";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import MenuItem from "@mui/material/MenuItem";
import Menu from "@mui/material/Menu";
import PopupState, { bindTrigger, bindMenu } from "material-ui-popup-state";
import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import { Grid } from "tabler-react";
import TextField from "@mui/material/TextField";
import Select from "@mui/material/Select";
import FormControl from "@mui/material/FormControl";
import Tooltip from "@mui/material/Tooltip";
import IconButton from "@mui/material/IconButton";
import {
  AddCircleOutlineOutlined,
  SubjectOutlined,
  PsychologyAlt,
  Psychology,
  Language,
  Chalet,
  Settings,
  Search,
  AccountBalance
} from "@mui/icons-material/";
import HikingIcon from "@mui/icons-material/Hiking";

import Navbar from "./Navbar.jsx";

export default function Layout(props) {
  let navigate = useNavigate();
  let location = useLocation();

  const handleAddThought = () => {
    navigate("add-thought");
  }

  const handleGlobalSearch = () => {
    navigate("organize-thoughts"); //TODO figure out how to handle already being on the page
  }
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
      icon: <Chalet color="secondary" />,
      path: "/",
    },
    {
      text: "Add Thought",
      icon: <AddCircleOutlineOutlined color="secondary" />,
      path: "/add-thought",
    },
    {
      text: "Thought Bank",
      icon: <AccountBalance color="secondary" />,
      path: "/thought-bank",
    },
    {
      text: "Organize Thoughts",
      icon: <Psychology color="secondary" />,
      path: "/organize-thoughts",
    },
    {
      text: "Outdoor Life",
      icon: <HikingIcon color="secondary" />,
      path: "/outdoor-activity-logs",
    },
    {
      text: "Spanish Words",
      icon: <Language color="secondary" />,
      path: "/words",
    },

    {
      text: "Settings",
      icon: <Settings color="secondary" />,
      path: "/settings",
    },
  ];

  return (
    <div className="">
      <Grid container>
        <Grid.Row className="no-margin">
          {/* <Navbar></Navbar> */}
          <AppBar
            position="fixed"
            className="appBar"
            elevation={0}
            color="primary"
          >
            <Toolbar>
              <Grid.Col xs={3}>
                <Typography variant="h6" color="inherit">
                  Bucket Of Thoughts
                </Typography>
              </Grid.Col>
              <Grid.Col xs={6}>
                <FormControl sx={{ width: "26%" }}>
                  <Select
                    labelId="global-search-select-label"
                    id="global-search-select"
                    name="globalSearchSelect"
                    variant="filled"
                    value={1}
                    sx={{ color: "white" }}
                  >
                    <MenuItem key={1} value={1}>
                      All
                    </MenuItem>
                    <MenuItem key={2} value={2}>
                      Thought
                    </MenuItem>
                  </Select>
                </FormControl>
                <TextField
                  sx={{ width: "58%", input: { color: "white" }, label: {color: "white"} }}
                  name="global-search"
                  label="Search Thoughts"
                  variant="filled"
                  onChange={props.globalSearchChange}
                />
                <IconButton color="secondary" aria-label="Search">
                  <Tooltip title="Search">
                    <Search sx={{ marginTop:"10px"}} onClick={handleGlobalSearch}></Search>
                  </Tooltip>
                </IconButton>
                <IconButton color="secondary" aria-label="Add" onClick={handleAddThought}>
                  <Tooltip title="Add Thought">
                    <AddCircleOutlineOutlined sx={{ marginTop:"10px"}}></AddCircleOutlineOutlined>
                  </Tooltip>
                </IconButton>
              </Grid.Col>
            </Toolbar>
          </AppBar>
        </Grid.Row>
        <Grid.Row className="no-margin">
          <Drawer
            className="drawer"
            variant="permanent"
            anchor="left"
            classes={{ paper: "drawer" }}
          >
            <List>
              {menuItems.map((item) => (
                <ListItem
                  key={item.text}
                  onClick={() => navigate(item.path)}
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
          <PopupState variant="popover" popupId="popup-menu">
            {(popupState) => (
              <React.Fragment>
                <Grid.Col xs={12}>
                  <Button
                    className="popover"
                    variant="contained"
                    {...bindTrigger(popupState)}
                  >
                    Bucket of Thoughts
                  </Button>
                </Grid.Col>
                <Grid.Col xs={12}>
                  <FormControl sx={{ width: "30%" }}>
                    <Select
                      labelId="global-search-select-label"
                      id="global-search-select"
                      name="globalSearchSelect"
                      className="popover"
                      value={1}
                    >
                      <MenuItem key={1} value={1}>
                        All
                      </MenuItem>
                      <MenuItem key={2} value={2}>
                        Thought
                      </MenuItem>
                    </Select>
                  </FormControl>
                  <TextField
                    sx={{ width: "70%" }}
                    name="global-search"
                    label="Search"
                    className="popover"
                  />
                </Grid.Col>
                <Menu {...bindMenu(popupState)}>
                  {menuItems.map((item) => (
                    <MenuItem
                      onClick={() => {
                        navigate(item.path);
                        popupState.close();
                      }}
                    >
                      <ListItemIcon>{item.icon}</ListItemIcon>
                      <ListItemText primary={item.text} />
                    </MenuItem>
                  ))}
                </Menu>
              </React.Fragment>
            )}
          </PopupState>
        </Grid.Row>
      </Grid>
      <div className="page">
        <div className="toolBar">{props.children}</div>
      </div>
    </div>
  );
}
