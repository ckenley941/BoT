import React, { useEffect, useState } from "react";
import Grid from "@mui/material/Unstable_Grid2";
import { DataGrid, GridToolbar  } from '@mui/x-data-grid';


export default function JsonDataGrid(props) {
const [columns, setColumns] = useState([]);
const [jsonData, setJsonData] = useState([]);

useEffect(() => {
  parseColumns();
  parseData();
}, []);

const parseColumns = () => {
  let dataColumns = [];
  var columns = props.columns.split("|");
  columns.forEach(c => {
    dataColumns.push({
      field: c,
      headerName: c,
      width: 150,
      editable: false,
    });
  });

  setColumns(dataColumns);
}

const parseData = () => {
  let jsonData = JSON.parse(props.jsonString);
  jsonData.forEach((jd, i) => {
    console.log(i + 1);
    jd.Id = i + 1;
  })
  setJsonData(jsonData);
}

  return (
    <Grid>
      <DataGrid
      getRowId={(row) => row.Id}
        rows={jsonData}
        columns={columns}
        initialState={{
          pagination: {
            paginationModel: {
              pageSize: 25,
            },
          },
        }}
        //onRowClick={handleRowClick} {...jsonData} 
        pageSizeOptions={[10, 25, 50]}
        checkboxSelection
        disableRowSelectionOnClick
        slots={{ toolbar: GridToolbar }}
        slotProps={{
          toolbar: {
            showQuickFilter: true,
          },
        }}
      />
   </Grid>
  );
}
