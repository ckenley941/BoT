import React, { useState, useEffect } from "react";

import Grid from "@mui/material/Unstable_Grid2";
import MenuItem from "@mui/material/MenuItem";
import Select from "@mui/material/Select";
import InputLabel from "@mui/material/InputLabel";
import FormControl from "@mui/material/FormControl";
import Tooltip from "@mui/material/Tooltip";

import RefreshIcon from "@mui/icons-material/Refresh";
import IconButton from "@mui/material/IconButton";

import SelectedDashboard from "./SelectedDashboard.jsx";

import { getSelectedDashboard } from "../../services/ThoughtsService.ts";
import { getThoughtBuckets } from "../../services/ThoughtsService.ts";

export default function Dashboard() {
  const [currentlySelectedDashboard, setCurrentlySelectedDashboard] = useState("RandomThought");
  const [selectedBucketId, setSelectedBucketId] = useState(0);
  const [data, setData] = useState(null);
  const [showBuckets, setShowBuckets] = useState(true); //setting to true for now while defaulting to RandomThought
  const [thoughtBuckets, setThoughtBuckets] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    loadBuckets();
  }, []);

  useEffect(() => {
    console.log('effect');
    loadData();
  }, [currentlySelectedDashboard, selectedBucketId]);


  const loadBuckets = async () => {
    getThoughtBuckets().then((response) => {
      setThoughtBuckets(response.data);
    });
  };

  const handleInputChange = (e) => {
    //setData(null);
    setShowBuckets(e.target.value === "RandomThought");
    setCurrentlySelectedDashboard(e.target.value);
  };

  const handleCategoryChange = (e) => {
    setSelectedBucketId(e.target.value);
  };

  const loadData = () => {
    setIsLoading(true);
    getSelectedDashboard(currentlySelectedDashboard, selectedBucketId).then((response) => {
      setData(response.data.data[0]);
      setIsLoading(false);
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
            <MenuItem key={"RecentThought"} value={"RecentThought"}>
              Recent Thought
            </MenuItem>
            <MenuItem key={"RandomWord"} value={"RandomWord"}>
              Random Word
            </MenuItem>
            <MenuItem key={"RandomOutdoorActivity"} value={"RandomOutdoorActivity"}>
              Random Outdoor Activity
            </MenuItem>
            <MenuItem key={"RandomConcert"} value={"RandomConcert"}>
              Random Concert
            </MenuItem>
          </Select>
        </FormControl>
      </Grid>
      {showBuckets ? (
        <Grid xs={4}>
          <FormControl fullWidth>
            <InputLabel id="dashboard-bucket-select-label">
              Bucket
            </InputLabel>
            <Select
              labelId="dashboard-bucket-select-label"
              id="dashboard-bucket-select"
              name="bucket"
              label="Bucket"
              maxWidth
              value={selectedBucketId}
              onChange={handleCategoryChange}
            >
              <MenuItem value="0">
                <em>None</em>
              </MenuItem>
              {thoughtBuckets.map((tc, i) => (
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
        <IconButton color="secondary" aria-label="Refresh" onClick={loadData}>          
          <Tooltip title="Refresh">
            <RefreshIcon />
          </Tooltip>
        </IconButton>
      </Grid>
      {!isLoading && (
      <Grid xs={12}>
        <SelectedDashboard
          selected={currentlySelectedDashboard}
          data={data}
        ></SelectedDashboard>
      </Grid>)}
    </Grid>
  );
}
