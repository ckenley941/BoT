import React, { useState, useEffect } from "react";

import Grid from "@mui/material/Unstable_Grid2";
import MenuItem from "@mui/material/MenuItem";
import Select from "@mui/material/Select";
import InputLabel from "@mui/material/InputLabel";
import FormControl from "@mui/material/FormControl";

import RefreshIcon from "@mui/icons-material/Refresh";
import IconButton from "@mui/material/IconButton";

import SelectedDashboard from "./SelectedDashboard.jsx";

import { getSelectedDashboard } from "../../services/ThoughtsService.ts";
import { getThoughtCategories } from "../../services/ThoughtsService.ts";

export default function Dashboard() {
  const [currentlySelectedDashboard, setCurrentlySelectedDashboard] =
    useState("RandomThought");
  const [data, setData] = useState(null);
  const [showCategories, setShowCategories] = useState(true); //setting to true for now while defaulting to RandomThought
  const [thoughtCategories, setThoughtCategories] = useState([]);

  useEffect(() => {
    loadCategories();
  }, []);

  const loadCategories = async () => {
    getThoughtCategories().then((response) => {
      setThoughtCategories(response.data);
    });
  };

  const handleInputChange = (e) => {
    setData(null);
    setShowCategories(e.target.value === "RandomThought");
    setCurrentlySelectedDashboard(e.target.value);
  };

  const refresh = () => {
    getSelectedDashboard(currentlySelectedDashboard).then((response) => {
      setData(response.data.data[0]);
    });
  };

  return (
    <Grid container spacing={2} className="m-2">
      <Grid xs={4}>
        <FormControl fullWidth>
          <InputLabel id="dashboard-select-label">Dashboard Panel</InputLabel>
          <Select
            labelId="dashboard-select-label"
            id="dashboard-select"
            name="dashboard-select"
            label="Dashboard Panel"
            maxWidth
            value={currentlySelectedDashboard}
            onChange={handleInputChange}
          >
            <MenuItem key={"RandomThought"} value={"RandomThought"}>
              Random Thought
            </MenuItem>
            <MenuItem key={"RandomWord"} value={"RandomWord"}>
              Random Word
            </MenuItem>
          </Select>
        </FormControl>
      </Grid>
      {showCategories ? (
        <Grid xs={4}>
          <FormControl fullWidth>
            <InputLabel id="dashboard-category-select-label">
              Category
            </InputLabel>
            <Select
              labelId="dashboard-category-select-label"
              id="dashboard-category-select"
              name="category"
              label="Category"
              maxWidth
              value={currentlySelectedDashboard}
              onChange={handleInputChange}
            >
              <MenuItem value="0">
                <em>None</em>
              </MenuItem>
              {thoughtCategories.map((tc, i) => (
                <MenuItem key={i} value={tc.id}>
                  {tc.description}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>
      ) : (
        <></>
      )}
      <Grid xs={4}>
        <IconButton color="secondary" aria-label="Refresh" onClick={refresh}>
          <RefreshIcon />
        </IconButton>
      </Grid>
      <Grid xs={12}>
        <SelectedDashboard
          selected={currentlySelectedDashboard}
          data={data}
        ></SelectedDashboard>
      </Grid>
    </Grid>
  );
}
