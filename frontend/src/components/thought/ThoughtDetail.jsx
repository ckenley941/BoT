import Grid from "@mui/material/Unstable_Grid2";

export default function ThoughtDetail({ thoughtDetail }) 
{
    return (
      <Grid key={thoughtDetail.thoughtDetailId}>{thoughtDetail.description}</Grid>
    );
}

